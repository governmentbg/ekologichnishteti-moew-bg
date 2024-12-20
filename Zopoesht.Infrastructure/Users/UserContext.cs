using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Zopoesht.Infrastructure.Interfaces.Users;

namespace Zopoesht.Infrastructure.Users
{
    public  class UserContext : IUserContext
    {
        private ClaimsPrincipal _user;

        public int UserId => int.Parse(_user.Claims.Single(c => c.Type.Equals(JwtRegisteredClaimNames.Jti)).Value);
        public string Username => _user.Claims.Single(c => c.Type.Equals("username")).Value;
        public string Role => _user.Claims.Single(c => c.Type == ClaimTypes.Role).Value;
        public int? AuthorityId => GetAuthorityId();

        public UserContext(IHttpContextAccessor contextAccessor)
        {
            this._user = contextAccessor.HttpContext?.User;
        }

        private int? GetAuthorityId()
        {
            int authorityId;

            int.TryParse(_user.Claims.Single(c => c.Type.Equals("authorityId")).Value, out authorityId);

            if (authorityId == 0)
            {
                return null;
            }

            return authorityId;
        }
    }
}