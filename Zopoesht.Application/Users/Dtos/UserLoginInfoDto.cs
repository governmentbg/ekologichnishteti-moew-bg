using Zopoesht.Infrastructure.Helpers.Dtos;

namespace Zopoesht.Application.Users.Dtos
{
    public class UserLoginInfoDto
    {
        public string Fullname { get; set; }
        public string RoleAlias { get; set; }
        public string Token { get; set; }

        public NomenclatureDto Authority { get; set; }
    }
}