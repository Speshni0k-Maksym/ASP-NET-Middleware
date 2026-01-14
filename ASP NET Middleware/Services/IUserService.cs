namespace ASP_NET_Middleware.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);
        Task<ClaimsPrincipal> CreatePrincipalAsync(string username);
    }
}