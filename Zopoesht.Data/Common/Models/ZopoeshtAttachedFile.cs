using FileStorageNetCore.Models;
using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Common.Models
{
    public class ZopoeshtAttachedFile : AttachedFile, IEntity, IConcurrency
    {
        public int Id { get; set; }

        public int Version { get; set; }
    }
}
