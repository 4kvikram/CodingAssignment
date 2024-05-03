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


        public IActionResult Registration(int id = 0)
        {
            StudentViewModel studentViewModel = new StudentViewModel();
            if (id > 0)
            {
                studentViewModel = _studentService.GetStudent(id);
            }
            studentViewModel.States = _studentService.GetStates().ToList();
            return View(studentViewModel);
        }

        [HttpPost]
        public IActionResult Registration(StudentViewModel viewModel)
        {

            string[] allowedFileTypes = { "image/jpeg", "image/png" };
            if (viewModel.Id == 0)
            {
                //add new
                // Check if photo is not null and has content
                if (viewModel.Photo != null && viewModel.Photo.Length < 250 * 1024 && allowedFileTypes.Contains(viewModel.Photo.ContentType))
                {
                    if (ModelState.IsValid)
                    {
                        viewModel.PhotoPath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}/";

                        _studentService.SaveStudent(viewModel);
                        return RedirectToAction(nameof(StudentList));

                    }
                }
            }
            else
            {
                if (viewModel.Photo != null)
                {
                    if (viewModel.Photo.Length < 250 * 1024 && allowedFileTypes.Contains(viewModel.Photo.ContentType))
                    {
                        viewModel.PhotoPath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}/";
                    }
                    else
                    {
                        viewModel.States = _studentService.GetStates().ToList();
                        return View(viewModel);
                    }
                }

                _studentService.SaveStudent(viewModel);
                return RedirectToAction(nameof(StudentList));
                //update
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
