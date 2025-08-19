namespace StudentRegistryApp.Models
{
    public class StudentVm
    {
        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the student's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the student's age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the student's email address.
        /// </summary>
        public string Email { get; set; }
    }
}
