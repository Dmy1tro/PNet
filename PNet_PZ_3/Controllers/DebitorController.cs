using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PNet_PZ_3.Context;
using PNet_PZ_3.Models;

namespace PNet_PZ_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebitorController : ControllerBase
    {
        private readonly BankContext _dbContext;

        public DebitorController(BankContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var debitors = from debitor in _dbContext.Debitors
                           select debitor;

            return Ok(debitors.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var debitor = from deb in _dbContext.Debitors
                          where deb.Id == id
                          select deb;

            return Ok(debitor.FirstOrDefault());
        }

        [HttpGet("by-name/{name}")]
        public IActionResult GetByName(string name)
        {
            var debitors = from deb in _dbContext.Debitors
                           where deb.Name == name
                           orderby deb.Name
                           select deb;

            return Ok(debitors.ToList());
        }

        [HttpPut]
        public IActionResult Put(Debitor model)
        {
            var debitor = (from deb in _dbContext.Debitors
                           where deb.Id == model.Id
                           select deb).FirstOrDefault();

            if (debitor is null)
            {
                return BadRequest("Debitor not found");
            }

            debitor.Name = model.Name;
            debitor.PhoneNumber = model.PhoneNumber;
            debitor.PostNumber = model.PostNumber;

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}