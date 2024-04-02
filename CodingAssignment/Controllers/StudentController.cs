using CodingAssignment.Models;
using CodingAssignment.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodingAssignment.Controllers
{
    public class StudentController : Controller
    {


        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public JsonResult GetCities(int id)
        {
            return Json(_studentService.GetCities(id));
        }


        public IActionResult Registration()
        {
            StudentViewModel studentViewModel = new StudentViewModel();
            studentViewModel.States = _studentService.GetStates().ToList();
            return View(studentViewModel);
        }

        [HttpPost]
        public IActionResult Registration(StudentViewModel viewModel)
        {
            // Check if photo is not null and has content
            string[] allowedFileTypes = { "image/jpeg", "image/png" };
            if (viewModel.Photo != null && viewModel.Photo.Length < 250 * 1024 && allowedFileTypes.Contains(viewModel.Photo.ContentType))
            {
                if (ModelState.IsValid)
                {
                    viewModel.PhotoPath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}/";

                    _studentService.AddStudent(viewModel);

                    return RedirectToAction(nameof(StudentList));
                }
            }
            viewModel.States = _studentService.GetStates().ToList();
            return View(viewModel);
        }
        public IActionResult StudentList()
        {
            List<StudentViewModel> students = _studentService.GetAllStudents().ToList();
            return View(students);
        }
    }
}
