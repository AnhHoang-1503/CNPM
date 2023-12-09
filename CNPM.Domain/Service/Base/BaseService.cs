using AutoMapper;

namespace CNPM.Domain
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityCUDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;

        protected readonly IMapper _mapper;

        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Lay tat ca ban ghi
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntityDto>> GetAll()
        {
            var entities = await _baseRepository.GetAll();

            var entityDtos = _mapper.Map<IEnumerable<TEntityDto>>(entities);

            return entityDtos;
        }

        /// <summary>
        /// Lay ban ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> GetById(int id)
        {
            var entity = await _baseRepository.GetById(id);

            if (entity == null)
            {
                throw new Exception("Not found");
            }

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }

        /// <summary>
        /// tim ban ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntityDto?> FindById(int id)
        {
            var entity = await _baseRepository.FindById(id);

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }

        /// <summary>
        /// Tao ban ghi moi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task Create(TEntityCUDto entityCUDto)
        {
            var entity = MapCUDtoToEntity(entityCUDto);

            await _baseRepository.Create(entity);

            await _baseRepository.SaveChangeAsync();
        }

        /// <summary>
        /// Chinh sua ban ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task Update(int id, TEntityCUDto entityCUDto)
        {
            var entity = MapCUDtoToEntity(id, entityCUDto);

            await _baseRepository.Update(id, entity);

            await _baseRepository.SaveChangeAsync();
        }

        /// <summary>
        /// Xoa ban ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task Delete(int id)
        {
            await _baseRepository.Delete(id);

            await _baseRepository.SaveChangeAsync();
        }

        /// <summary>
        /// map for create
        /// </summary>
        /// <param name="userCUDto"></param>
        /// <returns></returns>
        public abstract TEntity MapCUDtoToEntity(TEntityCUDto entityCUDto);

        /// <summary>
        /// map for update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userCUDto"></param>
        /// <returns></returns>
        public abstract TEntity MapCUDtoToEntity(int id, TEntityCUDto entityCUDto);
    }
}
