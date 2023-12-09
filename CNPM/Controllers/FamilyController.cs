using CNPM.Controllers.Base;
using CNPM.Domain;
using CNPM.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace CNPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : BaseController<Family, FamilyDto, FamilyCUDto>
    {
        private readonly FamilyService _familyService;
        public FamilyController(FamilyService familyService) : base(familyService)
        {
            _familyService = familyService;
        }

        [HttpPost("household_spilit")]
        public async Task<IActionResult> HouseholdSpilit([FromBody] FamilySpilitDto familySpilitDto)
        {
            await _familyService.FamilySpilit(familySpilitDto);

            return StatusCode(StatusCodes.Status200OK);
        }

    }
}
