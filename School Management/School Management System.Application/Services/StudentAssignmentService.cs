using Microsoft.AspNetCore.Http;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

public class StudentAssignmentService : IStudentAssignmentService
{
    private readonly IAssignmentRepository _assignmentRepo;
    private readonly ISubmissionRepository _submissionRepo;

    public StudentAssignmentService(
        IAssignmentRepository assignmentRepo,
        ISubmissionRepository submissionRepo)
    {
        _assignmentRepo = assignmentRepo;
        _submissionRepo = submissionRepo;
    }

    public async Task<IEnumerable<Assignment>> GetStudentAssignments(int studentId)
        => await _assignmentRepo.GetAssignmentsForStudent(studentId);

    public async Task<Submission> SubmitAssignmentAsync(int assignmentId, int studentId, IFormFile file)
    {
        var folderPath = Path.Combine("wwwroot", "submissions");
        Directory.CreateDirectory(folderPath);

        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(folderPath, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
            await file.CopyToAsync(stream);

        var submission = new Submission
        {
            AssignmentId  = assignmentId,
            StudentId     = studentId,
            SubmittedDate = DateTime.UtcNow,
            FileUrl       = filePath
        };

        await _submissionRepo.AddAsync(submission);
        await _submissionRepo.SaveChangesAsync(); // ✓ FIX
        return submission;
    }

    public async Task<IEnumerable<Submission>> GetStudentGrades(int studentId)
        => await _submissionRepo.GetByStudentIdAsync(studentId);
}
