using Microsoft.EntityFrameworkCore;
using MintaProjekt.Models;

namespace MintaProjektAPI.Data
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }
    }
}
