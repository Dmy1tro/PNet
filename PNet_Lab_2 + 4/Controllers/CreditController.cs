using Microsoft.AspNetCore.Mvc;
using PNet_Lab_2.Models;
using PNet_Lab_2.Repositories;

namespace PNet_Lab_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly BankRepository _bankRepository;

        public CreditController(BankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        [HttpGet("{creditId}")]
        public IActionResult GetCredit(int creditId)
        {
            var credit = _bankRepository.GetCredit(creditId);

            return Ok(credit);
        }

        [HttpGet("DebitorCredits/{debitorId}")]
        public IActionResult GetDebitorCredits(int debitorId)
        {
            var credits = _bankRepository.GetDebitorCredits(debitorId);

            return Ok(credits);
        }

        [HttpPost]
        public IActionResult Create(Credit credit)
        {
            var result = _bankRepository.AddCredit(credit);

            return result.IsValid
                ? Ok()
                : BadRequest(result.Error) as IActionResult;
        }
    }
}