using System.Linq;
using System.Web.Http;
using PNet_Lab_3.Models;

namespace PNet_Lab_3.Controllers
{
    [RoutePrefix("api/Credits")]
    public class CreditController : ApiController
    {
        private readonly BankDbContext _bankDbContext;

        public CreditController()
        {
            _bankDbContext = new BankDbContext();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCredit(int creditId)
        {
            var credit = _bankDbContext.Credits
                .FirstOrDefault(x => x.Id == creditId);

            var creditDto = MapCreditToDto(credit);

            return Ok(creditDto);
        }

        [HttpGet]
        [Route("DebitorCredits")]
        public IHttpActionResult GetDebitorCredits(int debitorId)
        {
            var creditsDto = _bankDbContext.Credits
                .Where(x => x.DebitorId == debitorId)
                .AsEnumerable()
                .Select(x => MapCreditToDto(x))
                .ToList();

            return Ok(creditsDto);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(CreditDto creditDto)
        {
            var credit = MapDtoToCredit(creditDto);

            _bankDbContext.Credits.Add(credit);

            _bankDbContext.SaveChanges();

            return Ok();
        }

        private CreditDto MapCreditToDto(Credit credit)
        {
            return credit is null
                ? null
                : new CreditDto
                {
                    Id = credit.Id,
                    Amount = credit.Amount,
                    Balance = credit.Balance,
                    DebitorId = credit.DebitorId,
                    OpenDate = credit.OpenDate
                };
        }

        private Credit MapDtoToCredit(CreditDto creditDto)
        {
            return creditDto is null
            ? null
            : new Credit
            {
                Id = creditDto.Id,
                Amount = creditDto.Amount,
                Balance = creditDto.Balance,
                DebitorId = creditDto.DebitorId,
                OpenDate = creditDto.OpenDate
            };
        }
    }
}
