using Zopoesht.Data.Applications.ApplicationOne;
using Zopoesht.Data.Nomenclatures.Operators;

namespace Zopoesht.Application.Applications.ApplicationsOne.Dtos
{
    public class ApplicationOneActivityOfferingDto
    {
        public int Id { get; set; }

        public int ApplicationOneBasicId { get; set; }

        public ApplicationOneBasic ApplicationOneBasic { get; set; }

        public int ActivityOfferingId { get; set; }

        public ActivityOffering ActivityOffering { get; set; }
    }
}
