using CNPM.Controllers.Base;
using CNPM.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporaryPaperController : BaseController<TemporaryPaper, TemporaryPaperDto, TemporaryPaperCUDto>
    {
        public TemporaryPaperController(TemporaryPaperService temporaryPaperService) : base(temporaryPaperService)
        {
        }
    }
}
