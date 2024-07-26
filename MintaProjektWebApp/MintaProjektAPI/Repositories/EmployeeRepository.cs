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

        // Get all existing records
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        // Get only requested records
        public async Task<PaginatedResponse<Employee>> GetEmployees(PaginationRequest request)
        {
            int totalRecords = await _dbContext.Employees.CountAsync();
            var employees = await _dbContext.Employees
            .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
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
