namespace ASP_NET_Middleware.Services
{
    using System.Threading.Tasks;

    public interface IWishService
    {
        Task<string> WriteRandomWishAsync();
    }
}