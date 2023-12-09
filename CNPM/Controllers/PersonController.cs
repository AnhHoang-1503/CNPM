using CNPM.Controllers.Base;
using CNPM.Domain;
using CNPM.Domain.Dto.Fee;
using CNPM.Domain.Entity;
using CNPM.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace CNPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController<Person, PersonDto, PersonCUDto>
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService) : base(personService)
        {
            _personService = personService;
        }

        [HttpGet("family/{familyId}")]
        public async Task<IActionResult> FindPeopleByFamilyId(int familyId)
        {
            var people = await _personService.FindPeopleByFamilyId(familyId);

            return StatusCode(StatusCodes.Status200OK, people);
        }

        [HttpPost("newborn")]
        public async Task<IActionResult> CreateNewborn([FromBody] NewbornCUDto newbornCUDto)
        {
            await _personService.CreateNewborn(newbornCUDto);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("{id}/die")]
        public async Task<IActionResult> Die(int id)
        {
            await _personService.Die(id);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
