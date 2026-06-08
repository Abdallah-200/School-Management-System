using AutoMapper;
using School_Management_System.Application.DTOs.User;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

namespace School_Management_System.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;
        private readonly IMapper           _mapper;

        public UserService(IRepository<User> repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(CreateUserDTO dto)
        {
            try
            {
                var user = _mapper.Map<User>(dto);

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                user.CreatedDate = DateTime.UtcNow;

                await _repo.AddAsync(user);
                await _repo.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    ex.InnerException?.Message ?? ex.Message
                );
            }
        }

        public async Task<User?> GetUserByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<User>> GetAllUsersAsync()
            => await _repo.GetAllAsync();

        public async Task<UserDTO?> UpdateUserAsync(int id, UserDTO dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return null;
            _mapper.Map(dto, user);
            user.UpdatedDate = DateTime.UtcNow;
            _repo.Update(user);
            await _repo.SaveChangesAsync(); // ✓ FIX
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;
            _repo.Delete(user);
            await _repo.SaveChangesAsync(); // ✓ FIX
            return true;
        }
    }
}
