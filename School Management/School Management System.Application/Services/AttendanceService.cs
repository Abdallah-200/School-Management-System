using AutoMapper;
using School_Management_System.Application.DTOs.Attendance;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

namespace School_Management_System.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repo;
        private readonly IMapper               _mapper;

        public AttendanceService(IAttendanceRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Attendance> MarkAttendanceAsync(CreateAttendanceDTO dto, int teacherId)
        {
            var attendance = _mapper.Map<Attendance>(dto);
            // ✓ FIX: teacherId extracted from JWT by controller, not from DTO body
            attendance.MarkedByTeacherId = teacherId;
            attendance.CreatedDate       = DateTime.UtcNow;
            // ✓ FIX: Status is now AttendanceStatus enum in DTO — no risky Enum.Parse
            await _repo.AddAsync(attendance);
            await _repo.SaveChangesAsync(); // ✓ FIX: SaveChanges was missing
            return attendance;
        }

        public async Task<IEnumerable<Attendance>> GetClassAttendance(int classId)
            => await _repo.GetByClassIdAsync(classId);

        public async Task<IEnumerable<Attendance>> GetStudentAttendance(int studentId)
            => await _repo.GetByStudentIdAsync(studentId);
    }
}
