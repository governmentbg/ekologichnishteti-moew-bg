using Zopoesht.Application.Applications.Common.Dtos;
using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Common.Enums;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application.Applications.Common.Interfaces
{
    public interface IApplicationService<TDto> : ITransientService
        where TDto : ApplicationCommitDto
    {
        Task<TDto> GetById(int id);
        Task<List<TReturnDto>> GetByState<TReturnDto>(CommitState commitState);
        Task<ApplicationHistoryResultDto> GetHistory(int rootId, ApplicationType type);

        Task Create(TDto dto);
        Task ChangeApplicationState(int applicationId, CommitState commitState, string changeStateDescription, ApplicationType type);
        Task<TDto> SaveModification(TDto dto);
        Task<TDto> BeginModification(TDto dto, CommitState state, ApplicationType applicationType);
        Task<TDto> FinishModification(TDto dto, CommitState state, ApplicationType applicationType);
        Task CancelModification(TDto dto, ApplicationType applicationType);
        Task ChangeApplicationStateAndCreateHistory(int applicationId, CommitState state, ApplicationType applicationType);

        Task AddLock(int applicationId, int userId, ApplicationType applicationType);
        Task RemoveLock(int applicationId, ApplicationType applicationType);

    }
}
