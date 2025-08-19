using Microsoft.AspNetCore.Mvc;
using StudentRegistryApp.Helper;
using StudentRegistryApp.Models;
using UtilsLib.Repositories;
using UtilsLib.Tools;

namespace StudentRegistryApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentsRepository _studentRepo;
        private readonly IDataUploadUtil _dataUploadUtil;


        /// <summary>
        /// Initializes a new instance of the <see cref="StudentController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging operations.</param>
        /// <param name="studentRepo">The repository for student data access.</param>
        /// <param name="dataUploadUtil">Data upload util</param>
        public StudentController(ILogger<StudentController> logger,
            IStudentsRepository studentRepo,
            IDataUploadUtil dataUploadUtil)
        {
            _logger = logger;
            _studentRepo = studentRepo;
            _dataUploadUtil = dataUploadUtil;
        }

        /// <summary>
        /// Displays a list of all students.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> that renders the student list view.</returns>
        public IActionResult Index()
        {
            return View(_studentRepo.GetAllStudentsAsync().Result.Select(s => s.MapToStudentVm()));
        }

        /// <summary>
        /// Adds a new student to the repository.
        /// </summary>
        /// <param name="student">The student view model containing student data.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> that redirects to the index view if successful;
        /// otherwise, returns the add view with validation errors.
        /// </returns>
        [HttpPost]
        public IActionResult Add(StudentVm student)
        {
            if (ModelState.IsValid)
            {
                _studentRepo.AddStudentAsync(student.MapToStudent()).Wait();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult UploadData()
        {
            var res = _dataUploadUtil.UploadData(_studentRepo.GetAllStudentsAsync().Result);
            string responseMessage = res.IsSuccess ? "Data uploaded successfully." : $"Error: {res.Error}";
            return Ok(responseMessage);
        }
    }
}
