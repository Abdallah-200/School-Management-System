using AutoMapper;
using School_Management_System.Application.DTOs.Attendance;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Enums;
using School_Management_System.Domain.Interfaces;


namespace School_Management_System.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repo;
        private readonly IMapper mapper;

        public AttendanceService(IAttendanceRepository repo , IMapper mapper)
        {
            _repo = repo;
            this.mapper = mapper;
        }

        public async Task<Attendance> MarkAttendanceAsync(CreateAttendanceDTO dto)
        {
            var attendance = mapper.Map<Attendance>(dto);


            attendance.Status = Enum.Parse<AttendanceStatus>(dto.Status);
                
                attendance.CreatedDate = DateTime.UtcNow;
       
            await _repo.AddAsync(attendance);
            return attendance;
        }

        public async Task<IEnumerable<Attendance>> GetClassAttendance(int classId)
        {
            return await _repo.GetByClassIdAsync(classId);
        }

        public async Task<IEnumerable<Attendance>> GetStudentAttendance(int studentId)
        {
            return await _repo.GetByStudentIdAsync(studentId);
        }
    }

}
