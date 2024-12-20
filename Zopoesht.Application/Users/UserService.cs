using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Users.Dtos;
using Zopoesht.Application.Users.Interfaces;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Nomenclatures.Constants;
using Zopoesht.Data.Users;
using Zopoesht.Data.Users.Constants;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.Users.Interfaces;

namespace Zopoesht.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IAppDbContext context;
        private readonly IPasswordService passwordService;
        private readonly DomainValidationService validation;
        private readonly IMapper mapper;

        public UserService(
            IAppDbContext context, 
            IPasswordService passwordService, 
            DomainValidationService validation,
            IMapper mapper)
        {
            this.context = context;
            this.passwordService = passwordService;
            this.validation = validation;
            this.mapper = mapper;
        }

        public async Task<SearchResultItemDto<UserSearchResultDto>> GetFiltered(UserSearchFilterDto model, CancellationToken cancellationToken)
        {
            var users = context.Set<User>()
                .Include(u => u.Role)
                .Include(u => u.Authority)
                .AsQueryable();

            var filteredUsers = model
                .WhereBuilder(users)
                .OrderBy(u => u.Id)
                .ThenBy(u => u.FirstName)
                .ThenBy(u => u.MiddleName)
                .ThenBy(u => u.LastName);

            return new SearchResultItemDto<UserSearchResultDto>
            {
                TotalCount = filteredUsers.Count(),
                Items = await filteredUsers
                    .ProjectTo<UserSearchResultDto>(this.mapper.ConfigurationProvider)
                    .Skip(model.Offset)
                    .Take(model.Limit)
                    .ToListAsync()
            };
        }

        public async Task<UserDto> GetById(int id, CancellationToken cancellationToken)
        {
            var user = await context.Set<User>()
                .Include(u => u.Role)
                .Include(u => u.Authority)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_DoesNotExist);
            }

            return mapper.Map<User, UserDto>(user); ;
        }

        public async Task Add(UserDto model, CancellationToken cancellationToken)
        {
            var isEmailTaken = await context.Set<User>()
                .AnyAsync(u => u.Email.Trim().ToLower() == model.Email.Trim().ToLower());

            if (isEmailTaken)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_ExistingEmail);
            }

            //var isUsernameTaken = await context.Set<User>()
            //    .AnyAsync(u => u.Username.Trim().ToLower() == model.Username.Trim().ToLower());

            //if (isUsernameTaken)
            //{
            //    this.validation.ThrowErrorMessage(UserErrorCode.User_ExistingUsername);
            //}

            var user = new User(model.FirstName, model.MiddleName, model.LastName, model.Email, model.Phone, model.Role.Id, model.Position);

            if (model.AuthorityId != null)
            {
                user.UpdateAuthority(model.AuthorityId);
            }

            if (model.Role.Alias == UserRoleAliases.ADMINISTRATOR || model.Role.Alias == UserRoleAliases.MOSV)
            {
                var mosvId = await this.context.Set<Authority>()
                    .Where(s => s.Alias == AuthorityAliases.MOSV)
                    .Select(s => s.Id)
                    .SingleOrDefaultAsync();

                user.UpdateAuthority(mosvId);
            }

            string passwordSalt = passwordService.GenerateSalt(128);
            string passwordHash = passwordService.HashPassword(model.Password, passwordSalt);

            user.Activate(passwordHash, passwordSalt);

            await context.Set<User>().AddAsync(user);

            await context.SaveChangesAsync();
        }

        public async Task Update(UserDto model, CancellationToken cancellationToken)
        {
            var user = await context.Set<User>()
                .SingleOrDefaultAsync(u => u.Id == model.Id);

            if (user == null)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_DoesNotExist);
            }

            var isEmailTaken = await context.Set<User>()
                .AnyAsync(u => u.Email.Trim().ToLower() == model.Email.Trim().ToLower() && u.Id != model.Id);

            if (isEmailTaken)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_ExistingEmail);
            }

            //var isUsernameTaken = await context.Set<User>()
            //    .AnyAsync(u => u.Username.Trim().ToLower() == model.Username.Trim().ToLower() && u.Id != model.Id);

            //if (isUsernameTaken)
            //{
            //    this.validation.ThrowErrorMessage(UserErrorCode.User_ExistingUsername);
            //}

            user.Update(model.Email, model.Phone, model.FirstName, model.MiddleName, model.LastName, model.Role.Id, model.Position, model.Status);

            if(model.Authority != null)
            {
                user.UpdateAuthority(model.Authority.Id);
            }
            else
            {
                user.ClearAuthority();
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                string passwordSalt = passwordService.GenerateSalt(128);
                string passwordHash = passwordService.HashPassword(model.Password, passwordSalt);

                user.ChangePassword(passwordHash, passwordSalt);
            }

            await context.SaveChangesAsync();
        }

        public async Task ChangePassword(UserPasswordDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(validation);

            var user = await context.Set<User>()
                .SingleOrDefaultAsync(u => u.Id == model.UserId);

            bool isVerified = passwordService.VerifyHashedPassword(user.Password, model.CurrentPassword, user.PasswordSalt);

            if (!isVerified)
            {
                validation.ThrowErrorMessage(UserErrorCode.User_InvalidPassword);
            }

            string passwordSalt = passwordService.GenerateSalt(128);
            string passwordHash = passwordService.HashPassword(model.NewPassword, passwordSalt);

            user.ChangePassword(passwordHash, passwordSalt);

            await context.SaveChangesAsync();
        }
    }
}
