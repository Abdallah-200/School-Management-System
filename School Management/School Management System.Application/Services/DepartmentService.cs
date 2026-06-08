using AutoMapper;
using School_Management_System.Application.DTOs;
using School_Management_System.Application.DTOs.Department;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

public class DepartmentService : IDepartmentService
{
    private readonly IRepository<Department> _repo;
    private readonly IMapper                 _mapper;

    public DepartmentService(IRepository<Department> repo, IMapper mapper)
    {
        _repo   = repo;
        _mapper = mapper;
    }

    public async Task<Department> CreateDepartmentAsync(CreateDepartmentDTO dto)
    {
        var dep = _mapper.Map<Department>(dto);
        await _repo.AddAsync(dep);
        await _repo.SaveChangesAsync(); // ✓ FIX
        return dep;
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        => await _repo.GetAllAsync();

    public async Task<Department?> GetDepartmentByIdAsync(int id)
        => await _repo.GetByIdAsync(id);

    public async Task<DepartmentDTO?> UpdateDepartmentAsync(int id, UpdateDepartmentDTO dto)
    {
        var dep = await _repo.GetByIdAsync(id);
        if (dep == null) return null;
        _mapper.Map(dto, dep);
        dep.UpdatedDate = DateTime.UtcNow;
        _repo.Update(dep);
        await _repo.SaveChangesAsync(); // ✓ FIX
        return _mapper.Map<DepartmentDTO>(dep);
    }

    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        var dep = await _repo.GetByIdAsync(id);
        if (dep == null) return false;
        _repo.Delete(dep);
        await _repo.SaveChangesAsync(); // ✓ FIX
        return true;
    }
}
