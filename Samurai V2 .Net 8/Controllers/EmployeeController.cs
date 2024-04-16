using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samurai_V2_.Net_8.DTOs;
using Samurai_V2_.Net_8.IRepository;

namespace Samurai_V2_.Net_8.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
         private static IEmployeeRepo _bookRepo;
        
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepo bookRepo)
        {
            _logger = logger;
            _bookRepo = bookRepo;
        }

        [HttpPost("Save", Name = "CreateUpdate")]
        public async Task<IActionResult> CreateUpdate(EmployeeDtos b)
        {
            try
            {
                var createdBook = await _bookRepo.CreateBook(b);


                return StatusCode(StatusCodes.Status201Created, createdBook);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost("DeleteEmp", Name = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var createdBook = await _bookRepo.DeleteEmp(id);

                if (createdBook == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "book with id:"+ id + "was not found");
                }
                else
                {
                  return Ok(createdBook);
                }
              
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("GetAllEmp", Name = "GetAll")]
        public async Task<IActionResult> GetAllEmp(int PageSize, int PageNo)
        {
            try
            {
                var createdBook = await _bookRepo.GetAllEmp(PageSize, PageNo);

                if (createdBook == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No data found");
                }
                else
                {
                  return Ok(createdBook);
                }
              
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
