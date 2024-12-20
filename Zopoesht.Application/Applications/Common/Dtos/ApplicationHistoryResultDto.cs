namespace Zopoesht.Application.Applications.Common.Dtos
{
    public class ApplicationHistoryResultDto
    {
        public int RecentApplicationId { get; set; }

        public List<ApplicationHistoryDto> ApplicationHistoryDtos { get; set; }
    }
}
