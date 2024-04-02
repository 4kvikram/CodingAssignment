﻿using CodingAssignment.Data;
using CodingAssignment.Data.Entities;
using CodingAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingAssignment.Service
{
    public class StudentService : IStudentService
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IEnumerable<StateViewModel> GetStates()
        {
            var states = _context.States.Select(s => new StateViewModel
            {
                Id = s.Id,
                StateName = s.StateName
            }).ToList();
            return states;
        }

        public IEnumerable<CityViewModel> GetCities(int stateId)
        {
            var cities = _context.Cities.Where(c => c.StateId == stateId)
                                        .Select(c => new CityViewModel
                                        {
                                            Id = c.Id,
                                            CityName = c.CityName
                                        }
                                        ).ToList();
            return cities;
        }

        public void AddStudent(StudentViewModel studentViewModel)
        {
            var student = new Student
            {
                Name = studentViewModel.Name,
                Email = studentViewModel.Email,
                Mobile = studentViewModel.Mobile,
                CityId = studentViewModel.CityId,
                AboutYourself = studentViewModel.AboutYourself,
                PhotoPath= studentViewModel.PhotoPath+SavePhoto(studentViewModel.Photo)
               
            };

            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public StudentViewModel GetStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return null;

            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Mobile = student.Mobile,
                CityId = student.CityId,
                AboutYourself = student.AboutYourself
            };

            return studentViewModel;
        }

        public IEnumerable<StudentViewModel> GetAllStudents()
        {
            var students = _context.Students.Include(x=>x.City.State).Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Mobile = s.Mobile,
                CityName = s.City.CityName,
                StateName = s.City.State.StateName,
                AboutYourself = s.AboutYourself,
                PhotoPath=s.PhotoPath,
            }).ToList();

            return students;
        }


        private string SavePhoto(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                throw new ArgumentException("Photo cannot be null or empty.");

            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profilephoto");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }

            return Path.Combine("profilephoto", uniqueFileName);
        }
    }
}
