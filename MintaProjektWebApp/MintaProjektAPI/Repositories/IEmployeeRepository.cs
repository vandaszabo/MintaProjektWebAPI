using MintaProjekt.Models;
using MintaProjektAPI.Models;

namespace MintaProjektAPI.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<PaginatedResponse<Employee>> GetRequestedEmployees(int currentPage, int pageSize);
    }
}
