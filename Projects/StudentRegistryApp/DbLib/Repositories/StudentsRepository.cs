using UtilsLib.Context;
using UtilsLib.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UtilsLib.Repositories;

/// <summary>
/// Repository for performing CRUD operations on Student entities using Entity Framework Core.
/// </summary>
public class StudentsRepository : IStudentsRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="StudentsRepository"/> class.
    /// </summary>
    /// <param name="context">The database context to use for data operations.</param>
    public StudentsRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously adds a new student to the database.
    /// </summary>
    /// <param name="student">The student entity to add.</param>
    public async Task AddStudentAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously retrieves all students from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of students.</returns>
    public async Task<IEnumerable<Student>> GetAllStudentsAsync() =>
        await _context.Students.ToListAsync();

    /// <summary>
    /// Asynchronously retrieves a student by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the student if found; otherwise, null.</returns>
    public async Task<Student> GetStudentByIdAsync(int id)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        return student;
    }
}