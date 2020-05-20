using System;
using System.Linq;
using System.Web.Http;
using PNet_Lab_3.Models;

namespace PNet_Lab_3.Controllers
{
    [RoutePrefix("api/Payments")]
    public class PaymentController : ApiController
    {
        private readonly BankDbContext _bankDbContext;

        public PaymentController()
        {
            _bankDbContext = new BankDbContext();
        }

        [HttpGet]
        [Route("CreditPayments")]
        public IHttpActionResult CreditPayments(int creditId)
        {
            var paymentsDto = _bankDbContext.Payments
                .Where(x => x.CreditId == creditId)
                .AsEnumerable()
                .Select(x => MapPaymentToDto(x))
                .ToList();

            return Ok(paymentsDto);
        }

        [HttpGet]
        [Route("filter")]
        public IHttpActionResult Filter(int? fromAmount, int? toAmount, DateTime? fromDate, DateTime? toDate)
        {
            IQueryable<Payment> payments = _bankDbContext.Payments;

            if (fromAmount.HasValue)
            {
                payments = payments.Where(x => x.Amount >= fromAmount);
            }

            if (toAmount.HasValue)
            {
                payments = payments.Where(x => x.Amount <= toAmount);
            }

            if (fromDate.HasValue)
            {
                payments = payments.Where(x => x.PaymentDate >= fromDate);
            }

            if (toDate.HasValue)
            {
                payments = payments.Where(x => x.PaymentDate <= toDate);
            }

            var paymentsDto = payments
                .AsEnumerable()
                .Select(x => MapPaymentToDto(x))
                .ToList();
            
            return Ok(paymentsDto);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(PaymentDto paymentDto)
        {
            var payment = MapDtoToPayment(paymentDto);

            _bankDbContext.Payments.Add(payment);

            var credit = _bankDbContext.Credits
                .First(x => x.Id == payment.CreditId);

            credit.Balance -= payment.Amount;

            _bankDbContext.SaveChanges();

            return Ok();
        }

        private PaymentDto MapPaymentToDto(Payment payment)
        {
            return payment is null
            ? null
            : new PaymentDto
            {
                Id = payment.Id,
                Amount = payment.Amount,
                CreditId = payment.CreditId,
                PaymentDate = payment.PaymentDate
            };
        }

        private Payment MapDtoToPayment(PaymentDto paymentDto)
        {
            return paymentDto is null
            ? null
            : new Payment
            {
                Id = paymentDto.Id,
                Amount = paymentDto.Amount,
                CreditId = paymentDto.CreditId,
                PaymentDate = paymentDto.PaymentDate
            };
        }

    }
}
