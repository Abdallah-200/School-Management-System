using AutoMapper;
using School_Management_System.Application.DTOs.Assignment;
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
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _repo;
        private readonly IMapper mapper;

        public AssignmentService(IAssignmentRepository repo,IMapper mapper)
        {
            _repo = repo;
            this.mapper = mapper;
        }

        public async Task<Assignment> CreateAssignmentAsync(CreateAssignmentDTO dto)
        {
            var assignment = mapper.Map<Assignment>(dto);

                assignment.CreatedDate = DateTime.UtcNow;
           
            await _repo.AddAsync(assignment);
            return assignment;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByClass(int classId)
        {
            return await _repo.GetByClassIdAsync(classId);
        }
    }

}
