using Microsoft.AspNetCore.Mvc;
using PNet_Lab_2.Models;
using PNet_Lab_2.Repositories;

namespace PNet_Lab_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly BankRepository _bankRepository;

        public PaymentController(BankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        [HttpGet("{creditId}")]
        public IActionResult CreditPayments(int creditId)
        {
            var payments = _bankRepository.GetCreditPayments(creditId);

            return Ok(payments);
        }

        [HttpPost]
        public IActionResult Create(Payment payment)
        {
            var result = _bankRepository.AddPayment(payment);

            return result.IsValid
                ? Ok()
                : BadRequest(result.Error) as IActionResult;
        }
    }
}