using Microsoft.AspNetCore.Mvc;
using MintaProjekt.Models;
using MintaProjektAPI.Models;
using MintaProjektAPI.Repositories;

namespace MintaProjektAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository) 
        {
            _repository = repository;
        }


        // Get all employees
        [HttpGet("Employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            try
            {
                var employees = await _repository.GetAll();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll method in EmployeeController: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get only the requested employees
        [HttpPost("RequestedEmployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetRequestedEmployees([FromBody]PaginationRequest pageDTO)
        {
            try
            {
                var response = await _repository.GetRequestedEmployees(pageDTO.CurrentPage, pageDTO.PageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll method in EmployeeController: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
