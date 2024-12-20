using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Zopoesht.Application.Common;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Common.Extensions;
using Zopoesht.Application.Common.Interfaces;
using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Application.Nomenclatures.Interfaces;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;
using Zopoesht.Data.Users.Constants;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.Interfaces.Users;
using Zopoesht.Persistence.Contexts;

namespace Zopoesht.Application.Nomenclatures.Services
{
    public class NomenclatureService<TNomenclature> : INomenclatureService<TNomenclature>
       where TNomenclature : Nomenclature, IIncludeAll<TNomenclature>, new ()
    {
        private readonly IAppDbContext context;
        private readonly IUserContext userContext;
        private readonly DomainValidationService validation;

        public NomenclatureService(IAppDbContext context, IUserContext userContext, DomainValidationService validation)
        {
            this.context = context;
            this.userContext = userContext;
            this.validation = validation;
        }

        public async Task<SearchResultItemDto<TNomenclature>> GetNomenclaturesAsync<TFilter>(TFilter filter)
            where TFilter : BaseNomenclatureFilterDto<TNomenclature>
        {
            IQueryable<TNomenclature> query = new TNomenclature()
                .IncludeAll(this.context.Set<TNomenclature>())
                .AsNoTracking();

            query = filter.WhereBuilder(query);
            query = filter.OrderBuilder(query);

            var filteredNomenclatures = new SearchResultItemDto<TNomenclature>
            {
                TotalCount = query.Count()
            };

            if (filter.Limit.HasValue && filter.Offset.HasValue)
            {
                query = query.ApplyPagination(filter.Offset.Value, filter.Limit.Value);
            }

            filteredNomenclatures.Items = await query.ToListAsync();

            return filteredNomenclatures;
        }

        public async Task<IEnumerable<TDto>> SelectNomenclaturesAsync<TFilter, TDto>(TFilter filter, params int[] ids)
            where TFilter : BaseNomenclatureFilterDto<TNomenclature>
            where TDto : IMapping<TNomenclature, TDto>, new()
        {
            IQueryable<TNomenclature> query = new TNomenclature()
                .IncludeAll(this.context.Set<TNomenclature>())
                .AsNoTracking();

            query = filter.WhereBuilder(query);
            query = filter.OrderBuilder(query);

            if (filter.ExcludeAlreadyAdded)
            {
                query = filter.RemoveAlreadyAdded(query, ids);
            }

            if (filter.Limit.HasValue && filter.Offset.HasValue)
            {
                query = query.ApplyPagination(filter.Offset.Value, filter.Limit.Value);
            }

            return await query.Select(new TDto().Map()).ToListAsync();
        }

        public async Task<TNomenclature> GetSingleOrDefaultNomenclatureAsync(Expression<Func<TNomenclature, bool>> predicate)
        {
            var nomenclature = await context.Set<TNomenclature>()
                .SingleOrDefaultAsync(predicate);

            return nomenclature;
        }

        public async Task<TNomenclature> InsertNomenclatureAsync(TNomenclature model)
        {
            ValidatePermission();
            ValidateNomenclature(model);

            var maxViewOrder = await context.Set<TNomenclature>()
                    .MaxAsync(e => e.ViewOrder);
            model.ViewOrder = maxViewOrder.HasValue ? maxViewOrder.Value + 1 : 1;

            EntityService.ClearSkipProperties(model);
            context.Set<TNomenclature>().Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<TNomenclature> UpdateNomenclatureAsync(int id, TNomenclature model)
        {
            ValidatePermission();
            ValidateNomenclature(model);

            var original = await context.Set<TNomenclature>()
                .SingleOrDefaultAsync(e => e.Id == id);
            if (original == null)
            {
                return null;
            }

            original.Update(model);
            EntityService.Update(original, model, context as AppDbContext);
            await context.SaveChangesAsync();

            return model;
        }

        //public async Task DeleteNomenclatureAsync(int id)
        //{
        //    ValidatePermission();

        //    var forDelete = await context.Set<TNomenclature>()
        //        .SingleOrDefaultAsync(e => e.Id == id);
        //    if (forDelete != null)
        //    {
        //        try
        //        {
        //            EntityService.Remove(forDelete, context as AppDbContext);
        //            await context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException ex)
        //        {
        //            this.validation.ThrowErrorMessage(NomenclatureErrorCode.Nomenclature_CannotDelete);
        //        }
        //    }
        //}

        private void ValidateNomenclature(TNomenclature model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                validation.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }
        }

        private void ValidatePermission()
        {
            if (userContext.Role != UserRoleAliases.ADMINISTRATOR)
            {
                validation.ThrowErrorMessage(UserErrorCode.User_NotEnoughPermission);
            }
        }
    }
}