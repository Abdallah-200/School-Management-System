using AutoMapper;
using School_Management_System.Application.DTOs.Assignment;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

namespace School_Management_System.Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _repo;
        private readonly IMapper               _mapper;

        public AssignmentService(IAssignmentRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Assignment> CreateAssignmentAsync(CreateAssignmentDTO dto)
        {
            var assignment = _mapper.Map<Assignment>(dto);
            assignment.CreatedDate = DateTime.UtcNow;
            await _repo.AddAsync(assignment);
            await _repo.SaveChangesAsync(); // ✓ FIX
            return assignment;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByClass(int classId)
            => await _repo.GetByClassIdAsync(classId);
    }
}
