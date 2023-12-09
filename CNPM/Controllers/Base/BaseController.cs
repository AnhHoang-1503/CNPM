using CNPM.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CNPM.Controllers.Base
{
    public abstract class BaseController<TEntity, TEntityDto, TEntityCUDto> : ControllerBase
    {
        private readonly BaseService<TEntity, TEntityDto, TEntityCUDto> _baseService;

        protected BaseController(BaseService<TEntity, TEntityDto, TEntityCUDto> baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _baseService.GetAll();

            return StatusCode(StatusCodes.Status200OK, entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _baseService.GetById(id);

            return StatusCode(StatusCodes.Status200OK, entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TEntityCUDto entityCUDto)
        {
            await _baseService.Create(entityCUDto);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TEntityCUDto entityCUDto)
        {
            await _baseService.Update(id, entityCUDto);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _baseService.Delete(id);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
