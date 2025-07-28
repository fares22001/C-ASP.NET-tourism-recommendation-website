using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace trial.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [Required]
        public int TripId { get; set; }

        [ForeignKey("TripId")]
        public Trips Trip { get; set; }
    }
}