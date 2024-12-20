using System.Text;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Zopoesht.Infrastructure.Logs.Extensions
{
    public static class HttpRequestLogExtension
    {
        private static readonly List<string> excludedUrls = new()
        {
            "/api/FilesStorage",
            "/api/Login",
        };

        public static int? GetUserId(this HttpRequest request)
        {
            int? userId = null;

            try
            {
                if (request != null)
                {
                    var user = request.HttpContext.User;

                    if (user != null)
                    {
                        var userClaims = user.Claims;
                        var claim = user.Claims.SingleOrDefault(c => c.Type.Equals(JwtRegisteredClaimNames.Jti));

                        if (claim != null && int.TryParse(claim.Value, out int uId))
                        {
                            userId = uId;
                        }
                    }
                }
            }
            catch
            {
            }

            return userId;
        }

        public static async Task<string> GetRequestBody(this HttpRequest request, bool ignoreRequestBody)
        {
            if ((request?.Method == HttpMethods.Post || request?.Method == HttpMethods.Put)
                && request?.ContentLength > 0
                && !request.ContentType.Contains("multipart/form-data")
                && !excludedUrls.Contains(request?.Path)
                && !ignoreRequestBody)
            {
                request?.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(request?.ContentLength)];

                await request?.Body.ReadAsync(buffer, 0, buffer.Length);

                var requestContent = Encoding.UTF8.GetString(buffer).Replace("\0", "");

                request.Body.Position = 0;

                return requestContent;
            }

            return null;
        }
    }
}