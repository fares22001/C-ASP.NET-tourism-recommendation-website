
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace trial.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        [ForeignKey("Trips")]
        public int TripId { get; set; }

        public Trips Trip { get; set; }
    }
}