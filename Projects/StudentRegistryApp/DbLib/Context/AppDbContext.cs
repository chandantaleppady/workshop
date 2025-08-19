using UtilsLib.Models;
using Microsoft.EntityFrameworkCore;

namespace UtilsLib.Context;

/// <summary>
/// Represents the Entity Framework Core database context for the application.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the Students table.
    /// </summary>
    public DbSet<Student> Students { get; set; }
}
