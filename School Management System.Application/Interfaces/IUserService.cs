using School_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Application.DTOs.User;

namespace School_Management_System.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserDTO dto);
        Task<User?> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<UserDTO> UpdateUserAsync(int id, UserDTO dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
