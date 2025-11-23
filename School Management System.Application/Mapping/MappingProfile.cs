using AutoMapper;
using School_Management_System.Application.DTOs.Assignment;
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
            // User
            CreateMap<CreateUserDTO, User>();

            // Department
            CreateMap<CreateDepartmentDTO, Department>();

            // Course
            CreateMap<CreateCourseDTO, Course>();

            // Class
            CreateMap<CreateClassDTO, Class>();

            // Assignment
            CreateMap<CreateAssignmentDTO, Assignment>();

            // Submission
            CreateMap<CreateSubmissionDTO, Submission>();

            // Notification
            CreateMap<CreateNotificationDTO, Notification>();

            
        }
    }
}
