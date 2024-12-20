using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Users.Dtos;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application.Users.Interfaces
{
    public interface IUserService : ITransientService
    {
        Task<SearchResultItemDto<UserSearchResultDto>> GetFiltered(UserSearchFilterDto model, CancellationToken cancellationToken);

        Task<UserDto> GetById(int id, CancellationToken cancellationToken);

        Task Add(UserDto model, CancellationToken cancellationToken);

        Task Update(UserDto model, CancellationToken cancellationToken);

        Task ChangePassword(UserPasswordDto model, CancellationToken cancellationToken);
    }
}
