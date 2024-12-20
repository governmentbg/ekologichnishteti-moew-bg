
using FilesStorageNetCore.FormDataHelpers;
using FileStorageNetCore;
using FileStorageNetCore.Api;
using FileStorageNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Zopoesht.Hosting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesStorageController : FileStorageController
    {
        public FilesStorageController(BlobStorageService service)
            : base(service)
        {

        }

        [HttpPost]
        [DisableFormValueModelBinding]
        public override async Task<AttachedFile> PostFile(int? dbId)
        {
            return await base.PostFile(dbId);
        }
    }
}