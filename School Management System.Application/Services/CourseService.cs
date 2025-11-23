using AutoMapper;
using School_Management_System.Application.DTOs.Courses;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;


namespace School_Management_System.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }
        public async Task<CourseDTO> CreateCourseAsync(CreateCourseDTO dto)
        {
            var course = mapper.Map<Course>(dto);

            await courseRepository.AddAsync(course);

            var result = mapper.Map<CourseDTO>(course);

            return result;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
        {
            var list = await courseRepository.GetAllAsync();

            return mapper.Map<List<CourseDTO>>(list);
        }

        public async Task<CourseDTO?> GetCourseByIdAsync(int id)
        {
            var course = await courseRepository.GetByIdAsync(id);

            if (course == null) return null;

            return mapper.Map<CourseDTO>(course);
        }

        public async Task<IEnumerable<CourseDTO>> GetCoursesByDepartmentIdAsync(int departmentId)
        {
            var list = await courseRepository.GetCoursesByDepartmentIdAsync(departmentId);

            return mapper.Map<List<CourseDTO>>(list);
        }
        public async Task<CourseDTO> UpdateCourseAsync(int id, UpdateCourseDTO dto)
        {
            var course = await courseRepository.GetByIdAsync(id);
            if (course == null) return null;

            
            mapper.Map(dto, course);
            course.UpdatedDate = DateTime.UtcNow;

            courseRepository.Update(course);

            
            return mapper.Map<CourseDTO>(course);
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await courseRepository.GetByIdAsync(id);
            if (course == null) return false;

            courseRepository.Delete(course);
            return true;
        }
    }
}
