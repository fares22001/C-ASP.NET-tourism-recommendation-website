using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace trial.Models
{
    public class DBinterest
    {
        [Key]
        public int ID { get; set; }
        public String Name { get; set; }

        [Required]
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

       
    }
}
