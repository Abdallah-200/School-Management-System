using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Application.DTOs;
using School_Management_System.Application.DTOs.Department;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

public class DepartmentService : IDepartmentService
{
    private readonly IRepository<Department> repository;
    private readonly IMapper mapper;

    public DepartmentService(IRepository<Department> repository , IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    public async Task<Department> CreateDepartmentAsync(CreateDepartmentDTO dto)
    {
        var department = mapper.Map<Department>(dto);
        

        await repository.AddAsync(department);

        return department;    
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
    {
       return await repository.GetAllAsync();
    }

    public async Task<Department?> GetDepartmentByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }
    public async Task<DepartmentDTO> UpdateDepartmentAsync(int id, UpdateDepartmentDTO dto)
    {
        var dep = await repository.GetByIdAsync(id);
        if (dep == null) return null;

        mapper.Map(dto, dep);
        dep.UpdatedDate = DateTime.UtcNow;

         repository.Update(dep);
        return mapper.Map<DepartmentDTO>(dep);
    }
    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        var dep = await repository.GetByIdAsync(id);
        if (dep == null) return false;

        repository.Delete(dep);
        return true;
    }
}
