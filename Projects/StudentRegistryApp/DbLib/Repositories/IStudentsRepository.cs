using UtilsLib.Models;

namespace UtilsLib.Repositories;

/// <summary>
/// Defines methods for performing CRUD operations on Student entities.
/// </summary>
public interface IStudentsRepository
{
    /// <summary>
    /// Asynchronously retrieves all students from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of students.</returns>
    Task<IEnumerable<Student>> GetAllStudentsAsync();

    /// <summary>
    /// Asynchronously retrieves a student by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the student if found; otherwise, null.</returns>
    Task<Student> GetStudentByIdAsync(int id);

    /// <summary>
    /// Asynchronously adds a new student to the database.
    /// </summary>
    /// <param name="student">The student entity to add.</param>
    Task AddStudentAsync(Student student);
}
