using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Data.Users.Enums;

namespace Zopoesht.Application.Users.Dtos
{
    public class UserSearchResultDto
    {
        public int Id { get; set; }

        public string Fullname { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public RoleDto Role { get; set; }

        public AuthorityDto Authority { get; set; }

        public UserStatus Status { get; set; }
    }
}
