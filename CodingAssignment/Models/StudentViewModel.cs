using System.ComponentModel.DataAnnotations;

namespace CodingAssignment.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid Mobile Number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "State is required")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please selecte the city")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "About Yourself is required")]
        [MaxLength(250, ErrorMessage = "About Yourself should not exceed 250 characters")]
        public string AboutYourself { get; set; }

        [Required(ErrorMessage = "Photo is required")]
        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }
        public string? PhotoPath { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }

        public List<StateViewModel> States { get; set; } = new List<StateViewModel>();
    }
}
