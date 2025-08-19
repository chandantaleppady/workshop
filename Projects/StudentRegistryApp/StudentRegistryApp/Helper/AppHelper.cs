using StudentRegistryApp.Models;
using UtilsLib.Models;

namespace StudentRegistryApp.Helper
{
    public static class AppHelper
    {
        /// <summary>
        /// Maps a <see cref="StudentVm"/> instance to a <see cref="Student"/> instance.
        /// </summary>
        /// <param name="studentVm">The <see cref="StudentVm"/> object to map from.</param>
        /// <returns>A new <see cref="Student"/> object with properties copied from <paramref name="studentVm"/>.</returns>
        public static Student MapToStudent(this StudentVm studentVm)
        {
            return new Student
            {
                Id = studentVm.Id,
                Name = studentVm.Name,
                Age = studentVm.Age,
                Email = studentVm.Email
            };
        }

        /// <summary>
        /// Maps a <see cref="Student"/> instance to a <see cref="StudentVm"/> instance.
        /// </summary>
        /// <param name="student">The <see cref="Student"/> object to map from.</param>
        /// <returns>A new <see cref="StudentVm"/> object with properties copied from <paramref name="student"/>.</returns>
        public static StudentVm MapToStudentVm(this Student student)
        {
            return new StudentVm
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Email = student.Email
            };
        }
    }
}
