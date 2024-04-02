using System.ComponentModel.DataAnnotations;

namespace CodingAssignment.Data.Entities
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        public string StateName { get; set; }
       
        public ICollection<City> Cities { get; set; }
    }
}
