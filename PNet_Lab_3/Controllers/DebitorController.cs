using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using PNet_Lab_3.Models;

namespace PNet_Lab_3.Controllers
{
    [RoutePrefix("api/Debitors")]
    public class DebitorController : ApiController
    {
        private readonly BankDbContext _bankDbContext;

        public DebitorController()
        {
            _bankDbContext = new BankDbContext();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var debitorsDto = _bankDbContext.Debitors
                .AsEnumerable()
                .Select(x => MapDebitorToDto(x))
                .ToList();

            return Ok(debitorsDto);
        }

        [HttpGet]
        [Route("ByName")]
        public IHttpActionResult GetByName(string name)
        {
            var nameParameter = new SqlParameter("@name", name);

            var debitorsDto = _bankDbContext.Database.SqlQuery<Debitor>("GetDebitorsByName @name", nameParameter)
                .AsEnumerable()
                .Select(x => MapDebitorToDto(x));

            return Ok(debitorsDto);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(DebitorDto debitorDto)
        {
            var name = new SqlParameter("@name", debitorDto.Name);
            var postNumber = new SqlParameter("@postNumber", debitorDto.PostNumber);
            var phoneNumber = new SqlParameter("@phoneNumber", debitorDto.PhoneNumber);

            _bankDbContext.Database.ExecuteSqlCommand("CreateDebitor @name, @postNumber, @phoneNumber", 
                name, postNumber, phoneNumber);

            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Put(DebitorDto debitorDto)
        {
            var debitor = _bankDbContext.Debitors
                .FirstOrDefault(x => x.Id == debitorDto.Id);

            if (debitor is null)
            {
                return BadRequest("Debitor not found");
            }

            debitor.Name = debitorDto.Name;
            debitor.PhoneNumber = debitorDto.PhoneNumber;
            debitor.PostNumber = debitorDto.PostNumber;

            _bankDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete(int debitorId)
        {
            var debitor = _bankDbContext.Debitors
                .FirstOrDefault(x => x.Id == debitorId);

            if (debitor is null)
            {
                return BadRequest("Debitor not found");
            }

            _bankDbContext.Debitors.Remove(debitor);

            _bankDbContext.SaveChanges();

            return Ok();
        }

        private DebitorDto MapDebitorToDto(Debitor debitor)
        {
            return debitor is null
            ? null
            : new DebitorDto
            {
                Id = debitor.Id,
                Name = debitor.Name,
                PhoneNumber = debitor.PhoneNumber,
                PostNumber = debitor.PostNumber
            };
        }

        private Debitor MapDtoToDebitor(DebitorDto debitorDto)
        {
            return debitorDto is null
            ? null
            : new Debitor
            {
                Id = debitorDto.Id,
                Name = debitorDto.Name,
                PhoneNumber = debitorDto.PhoneNumber,
                PostNumber = debitorDto.PostNumber
            };
        }
    }
}
