using CodingAssignment.Models;

namespace CodingAssignment.Service
{
    public interface IStudentService
    {
        IEnumerable<StateViewModel> GetStates();
        IEnumerable<CityViewModel> GetCities(int stateId);
        void AddStudent(StudentViewModel studentViewModel);
        StudentViewModel GetStudent(int id);
        IEnumerable<StudentViewModel> GetAllStudents();
    }
}
