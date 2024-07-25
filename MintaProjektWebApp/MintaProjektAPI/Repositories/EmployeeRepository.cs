using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MintaProjekt.Models;
using MintaProjektAPI.Data;
using MintaProjektAPI.Models;

namespace MintaProjektAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<PaginatedResponse<Employee>> GetRequestedEmployees(int currentPage, int pageSize)
        {

            int totalRecords = await _dbContext.Employees.CountAsync();
            var employees = await _dbContext.Employees
            .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new PaginatedResponse<Employee>
            {
                TotalRecords = totalRecords,
                Data = employees
            };

            return response;
        }
    }
}
