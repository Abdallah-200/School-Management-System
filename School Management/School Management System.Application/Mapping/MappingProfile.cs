using AutoMapper;
using School_Management_System.Application.DTOs.Assignment;
using School_Management_System.Application.DTOs.Attendance;
using School_Management_System.Application.DTOs.Class;
using School_Management_System.Application.DTOs.Courses;
using School_Management_System.Application.DTOs.Department;
using School_Management_System.Application.DTOs.Notification;
using School_Management_System.Application.DTOs.Submission;
using School_Management_System.Application.DTOs.User;
using School_Management_System.Domain.Entities;

namespace School_Management_System.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ── User ──────────────────────────────────────────────
            CreateMap<CreateUserDTO, User>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.Name));
            CreateMap<User,      UserDTO>().ReverseMap();

            // ── Department ───────────────────────────────────────
            CreateMap<CreateDepartmentDTO, Department>();
            CreateMap<UpdateDepartmentDTO, Department>()
                .ForAllMembers(o => o.Condition((_, _, v) => v != null));
            CreateMap<Department, DepartmentDTO>();

            // ── Course ───────────────────────────────────────────
            CreateMap<CreateCourseDTO,  Course>();
            CreateMap<UpdateCourseDTO,  Course>()
                .ForAllMembers(o => o.Condition((_, _, v) => v != null));
            CreateMap<Course, CourseDTO>().ReverseMap();

            // ── Class ────────────────────────────────────────────
            CreateMap<CreateClassDTO, Class>();

            // ── Assignment ───────────────────────────────────────
            CreateMap<CreateAssignmentDTO, Assignment>();

            // ── Submission ───────────────────────────────────────
            // ✓ FIX: Ignore StudentId — set by service from JWT
            CreateMap<CreateSubmissionDTO, Submission>()
                .ForMember(d => d.StudentId, o => o.Ignore());

            // ── Notification ─────────────────────────────────────
            CreateMap<CreateNotificationDTO, Notification>();

            // ── Attendance ───────────────────────────────────────
            // ✓ FIX: Ignore MarkedByTeacherId — set by service from JWT
            CreateMap<CreateAttendanceDTO, Attendance>()
                .ForMember(d => d.MarkedByTeacherId, o => o.Ignore());
        }
    }
}
