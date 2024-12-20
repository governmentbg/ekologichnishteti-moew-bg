using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Application.Operators.Interfaces;
using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces.Contexts;

namespace Zopoesht.Application.Operators
{
    public class OperatorService : IOperatorService
    {
        private readonly IAppDbContext context;
        private readonly IMapper mapper;
        private readonly DomainValidationService validation;

        public OperatorService(
            IAppDbContext context,
            IMapper mapper,
            DomainValidationService validation)
        {
            this.context = context;
            this.mapper = mapper;
            this.validation = validation;
        }

        public async Task<OperatorDto> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await new Operator().IncludeAll(context.Set<Operator>().AsNoTracking())
               .SingleOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);

            if (result == null)
            {
                this.validation.ThrowErrorMessage(OperatorErrorCode.Operator_DoesNotExist);
            }

            var operatorData = mapper.Map<Operator, OperatorDto>(result);

            return operatorData;
        }

        public async Task<SearchResultItemDto<OperatorSearchResultDto>> GetFiltered(OperatorSearchFilterDto filter, CancellationToken cancellationToken)
        {
            if (filter == null)
            {
                filter = new OperatorSearchFilterDto();
            }

            var query = new Operator().IncludeAll(context.Set<Operator>().AsNoTracking());
            query = filter.WhereBuilder(query);

            query = query
                .OrderBy(e => e.Id);

            var result = new SearchResultItemDto<OperatorSearchResultDto>
            {
                TotalCount = await query.CountAsync(cancellationToken: cancellationToken),
                Items = await query.ProjectTo<OperatorSearchResultDto>(this.mapper.ConfigurationProvider)
                        .Skip(filter.Offset ?? 0)
                        .Take(filter.Limit ?? 0)
                        .ToListAsync(cancellationToken: cancellationToken)
            };

            return result;
        }

        public async Task Add(OperatorDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(validation);

            var oepratorData = mapper.Map<Operator>(model);

            await context.Set<Operator>().AddAsync(oepratorData);
            await context.SaveChangesAsync();
        }

        public async Task Update(OperatorDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(validation);

            var operatorData = await context.Set<Operator>()
                .SingleOrDefaultAsync(o => o.Id == model.Id);

            if (operatorData == null)
            {
                validation.ThrowErrorMessage(OperatorErrorCode.Operator_DoesNotExist);
            }

            operatorData = mapper.Map<OperatorDto, Operator>(model, operatorData);

            await context.SaveChangesAsync();
        }
    }
}
