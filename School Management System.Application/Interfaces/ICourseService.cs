using School_Management_System.Application.DTOs.Courses;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;


namespace School_Management_System.Application.Interfaces
{
    public interface ICourseService 
    {
        Task<CourseDTO> CreateCourseAsync(CreateCourseDTO dto);
        Task<IEnumerable<CourseDTO>> GetAllCoursesAsync();
        Task<CourseDTO?> GetCourseByIdAsync(int id);
        Task<IEnumerable<CourseDTO>> GetCoursesByDepartmentIdAsync(int departmentId);
        Task<CourseDTO> UpdateCourseAsync(int id, UpdateCourseDTO dto);
        Task<bool> DeleteCourseAsync(int id);
    }
}
