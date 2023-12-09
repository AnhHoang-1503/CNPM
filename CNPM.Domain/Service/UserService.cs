using AutoMapper;

namespace CNPM.Domain
{
    public class UserService : BaseService<User, UserDto, UserCUDto>
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> FindByUserName(string userName)
        {
            var user = await _userRepository.FindByUserName(userName);

            return user;
        }

        public async Task ChangePassword(int id, string newPassword)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                throw new Exception("Not found");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            await _userRepository.Update(id, user);
        }

        public override User MapCUDtoToEntity(UserCUDto userCUDto)
        {
            var user = _mapper.Map<User>(userCUDto);

            user.Role = Role.user;

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            return user;
        }

        public override User MapCUDtoToEntity(int id, UserCUDto userCUDto)
        {
            var user = _mapper.Map<User>(userCUDto);

            user.Id = id;

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            return user;
        }
    }
}
