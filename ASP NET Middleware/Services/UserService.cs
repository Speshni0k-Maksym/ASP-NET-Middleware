namespace ASP_NET_Middleware.Services
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.Cookies;

    public class UserService : IUserService
    {
        private readonly Dictionary<string, string> _users = new()
        {
            ["admin"] = "password",
            ["user"] = "password"
        };

        private readonly Dictionary<string, string> _roles = new()
        {
            ["admin"] = "Admin",
            ["user"] = "User"
        };

        public Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var ok = !string.IsNullOrEmpty(username) && _users.TryGetValue(username, out var pw) && pw == password;
            return Task.FromResult(ok);
        }

        public Task<ClaimsPrincipal> CreatePrincipalAsync(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            if (_roles.TryGetValue(username, out var role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            return Task.FromResult(principal);
        }
    }
}