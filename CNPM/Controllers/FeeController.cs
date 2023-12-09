using CNPM.Controllers.Base;
using CNPM.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeController : BaseController<Fee, FeeDto, FeeCUDto>
    {
        private readonly FeeService _feeService;
        public FeeController(FeeService feeService) : base(feeService)
        {
            _feeService = feeService;
        }

        [HttpPost("cleaning-fee")]
        public async Task<IActionResult> CreateCleaningFee()
        {
            await _feeService.CreateCleaningFee();

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet("total-amount/{id}")]
        public async Task<IActionResult> GetTotalAmount(int id)
        {
            var totalAmount = await _feeService.GetTotalAmount(id);

            return StatusCode(StatusCodes.Status200OK, totalAmount);
        }

        [HttpGet("fee-list/{id}")]
        public async Task<IActionResult> GetFeeList(int id)
        {
            var feeList = await _feeService.GetFeeList(id);

            return StatusCode(StatusCodes.Status200OK, feeList);
        }
    }
}
