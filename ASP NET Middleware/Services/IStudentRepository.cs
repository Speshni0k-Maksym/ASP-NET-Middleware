namespace ASP_NET_Middleware.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ASP_NET_Middleware.Models;

    public interface IStudentRepository
    {
        Task AddAsync(Student student);
        Task<IEnumerable<Student>> GetAllAsync();
    }
}