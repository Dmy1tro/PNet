using Microsoft.AspNetCore.Mvc;
using PNet_Lab_2.Models;
using PNet_Lab_2.Repositories;

namespace PNet_Lab_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebitorController : ControllerBase
    {
        private readonly DapperRepository _dapperRepository;

        public DebitorController(DapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var debitors = _dapperRepository.GetAllDebitors();

            return Ok(debitors);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var debitors = _dapperRepository.SearchDebitors(name);

            return Ok(debitors);
        }

        [HttpPost]
        public IActionResult Create(Debitor debitor)
        {
            var result = _dapperRepository.AddDebitor(debitor);

            return result.IsValid
                ? NoContent()
                : BadRequest(result.Error) as IActionResult;
        }

        [HttpPut]
        public IActionResult Put(Debitor debitor)
        {
            var result = _dapperRepository.UpdateDebitor(debitor);

            return result.IsValid
                ? NoContent()
                : BadRequest(result.Error) as IActionResult;
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var result = _dapperRepository.DeleteDebitor(id);

            return result.IsValid
                ? NoContent()
                : BadRequest(result.Error) as IActionResult;
        }
    }
}