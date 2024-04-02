using System.ComponentModel.DataAnnotations;

namespace CodingAssignment.Data.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public int CityId { get; set; }
        
        public City City { get; set; }

        [Required]
        public string AboutYourself { get; set; }
        [Required]
        public string PhotoPath { get; set; }
    }
}
