using AutoMapper;
using School_Management_System.Application.DTOs.Class;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;

public class ClassService : IClassService
{
    private readonly IClassRepository _classRepository;
    private readonly IMapper mapper;

    public ClassService(IClassRepository classRepository, IMapper mapper)
    {
        _classRepository = classRepository;
        this.mapper = mapper;
    }

    public async Task<Class> CreateClassAsync(CreateClassDTO dto )
    {
        var newClass = mapper.Map<Class>(dto);

        await _classRepository.AddAsync(newClass);

        return newClass;
       
    }

    public async Task<IEnumerable<Class>> GetAllAsync()
    {
        return await _classRepository.GetAllAsync();
    }
}
