namespace UtilsLib.Models;

/// <summary>
/// Represents a student with basic information such as Id, Name, Age, and Email.
/// </summary>
public class Student
{
    /// <summary>
    /// Gets or sets the unique identifier for the student.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the student.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the age of the student.
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Gets or sets the email address of the student.
    /// </summary>
    public string Email { get; set; }
}
