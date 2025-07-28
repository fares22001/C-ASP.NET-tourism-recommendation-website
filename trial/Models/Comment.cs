using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trial.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

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