using AutoMapper;
using School_Management_System.Application.DTOs.Courses;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

namespace School_Management_System.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;
        private readonly IMapper           _mapper;

        public CourseService(ICourseRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<CourseDTO> CreateCourseAsync(CreateCourseDTO dto)
        {
            var course = _mapper.Map<Course>(dto);
            await _repo.AddAsync(course);
            await _repo.SaveChangesAsync(); // ✓ FIX
            return _mapper.Map<CourseDTO>(course);
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
            => _mapper.Map<List<CourseDTO>>(await _repo.GetAllAsync());

        public async Task<CourseDTO?> GetCourseByIdAsync(int id)
        {
            var course = await _repo.GetByIdAsync(id);
            return course == null ? null : _mapper.Map<CourseDTO>(course);
        }

        public async Task<IEnumerable<CourseDTO>> GetCoursesByDepartmentIdAsync(int departmentId)
            => _mapper.Map<List<CourseDTO>>(await _repo.GetCoursesByDepartmentIdAsync(departmentId));

        public async Task<CourseDTO?> UpdateCourseAsync(int id, UpdateCourseDTO dto)
        {
            var course = await _repo.GetByIdAsync(id);
            if (course == null) return null;
            _mapper.Map(dto, course);
            course.UpdatedDate = DateTime.UtcNow;
            _repo.Update(course);
            await _repo.SaveChangesAsync(); // ✓ FIX
            return _mapper.Map<CourseDTO>(course);
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _repo.GetByIdAsync(id);
            if (course == null) return false;
            _repo.Delete(course);
            await _repo.SaveChangesAsync(); // ✓ FIX
            return true;
        }
    }
}
