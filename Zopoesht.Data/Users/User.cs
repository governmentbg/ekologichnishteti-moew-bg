using System.Text.Json.Serialization;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Users.Enums;

namespace Zopoesht.Data.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public string PasswordSalt { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public UserStatus Status { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int? AuthorityId { get; set; }
        public Authority Authority { get; set; }

        public string Position { get; set; }

        public User(string firstName, string middleName, string lastName, string email, string phone, int roleId, string position)
        {
            this.Username = email?.Trim().ToLower();
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.Email = email?.Trim().ToLower();
            this.Phone = phone;
            this.RoleId = roleId;
            this.Position = position;

            this.CreateDate = DateTime.UtcNow;
            this.Status = UserStatus.Inactive;
        }

        public void Activate(string passwordHash, string passwordSalt)
        {
            this.Password = passwordHash;
            this.PasswordSalt = passwordSalt;
            this.UpdateDate = DateTime.UtcNow;
            this.Status = UserStatus.Active;
        }

        public void ChangePassword(string passwordHash, string passwordSalt)
        {
            this.Password = passwordHash;
            this.PasswordSalt = passwordSalt;
            this.UpdateDate = DateTime.UtcNow;
        }

        public void Update(string email, string phone, string firstName, string middleName, string lastName, int roleId, string position, UserStatus userStatus)
        {
            this.Username = email?.Trim().ToLower();
            this.Email = email?.Trim().ToLower();
            this.Phone = phone;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.RoleId = roleId;
            this.Position = position;
            this.Status = userStatus;
        }

        public void UpdateAuthority(int? authorityId)
        {
            this.AuthorityId = authorityId;
        }

        public void ClearAuthority()
        {
            this.AuthorityId = null;
        }
    }
}