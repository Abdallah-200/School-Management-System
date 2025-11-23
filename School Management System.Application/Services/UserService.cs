using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Application.DTOs.User;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.Services
{
        public class UserService : IUserService
            {
                private readonly IRepository<User> _userRepository;
                private readonly IMapper mapper;

        public UserService(IRepository<User> userRepository , IMapper mapper)
                {
                    _userRepository = userRepository;
                    this.mapper = mapper;
                }

        public async Task<User> CreateUserAsync(CreateUserDTO dto)
                {
                    var user = mapper.Map<User>(dto);

                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                
                    user.CreatedDate = DateTime.UtcNow;
                   
                    await _userRepository.AddAsync(user);
                    
                    return user;
                }

        public async Task<User?> GetUserByIdAsync(int id)
                {
                    return await _userRepository.GetByIdAsync(id);
                }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
                {
                    return await _userRepository.GetAllAsync();
                }
        public async Task<UserDTO> UpdateUserAsync(int id, UserDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            mapper.Map(dto, user);
            user.UpdatedDate = DateTime.UtcNow;

            _userRepository.Update(user);
            return mapper.Map<UserDTO>(user);
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            _userRepository.Delete(user);
            return true;
        }
    }
    }

