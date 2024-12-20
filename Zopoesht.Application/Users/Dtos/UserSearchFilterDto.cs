using Zopoesht.Data.Users;
using Zopoesht.Data.Users.Enums;

namespace Zopoesht.Application.Users.Dtos
{
    public class UserSearchFilterDto
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public UserStatus? Status { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }

        public IQueryable<User> WhereBuilder(IQueryable<User> query)
        {
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                query = query.Where(u => u.FirstName.Trim().ToLower().Contains(FirstName.Trim().ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(MiddleName))
            {
                query = query.Where(u => u.MiddleName.Trim().ToLower().Contains(MiddleName.Trim().ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(LastName))
            {
                query = query.Where(u => u.LastName.Trim().ToLower().Contains(LastName.Trim().ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(Email))
            {
                query = query.Where(u => u.Email.Trim().ToLower().Contains(Email.Trim().ToLower()));
            }

            if (Status.HasValue)
            {
                query = query.Where(u => u.Status == Status);
            }

            return query;
        }
    }
}
