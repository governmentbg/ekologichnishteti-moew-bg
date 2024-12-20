using Zopoesht.Data.Common.Models;

namespace Zopoesht.Application.Applications.Common.Dtos
{
    public class ApplicationFileDto
    {
        public string Description { get; set; }

        public int ZopoeshtAttachedFileId { get; set; }

        public ZopoeshtAttachedFile ZopoeshtAttachedFile { get; set; }
    }
}
