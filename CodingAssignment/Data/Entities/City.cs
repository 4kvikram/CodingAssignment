using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingAssignment.Data.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string CityName { get; set; }
        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }
    }
}
