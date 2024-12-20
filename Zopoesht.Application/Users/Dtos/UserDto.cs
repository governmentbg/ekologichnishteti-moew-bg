using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Data.Users.Enums;

namespace Zopoesht.Application.Users.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public RoleDto Role { get; set; }

        public int?  AuthorityId { get; set; }
        public AuthorityDto Authority { get; set; }

        public UserStatus Status { get; set; }

        public string Position { get; set; }
    }
}
