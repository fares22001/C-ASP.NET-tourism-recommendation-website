using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace trial.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Country Name")]
        public string CountryName { get; set; }

        [NotMapped]
        [Required]
        public IFormFile clientFile { get; set; }

        public byte[]? dbImage { get; set; }

        [NotMapped]
        public string? imageSrc
        {
            get
            {
                if (dbImage != null)
                {
                    string base64String = Convert.ToBase64String(dbImage, 0, dbImage.Length);
                    return "data:image/jpg;base64," + base64String;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public ICollection<Trips>? trips { get; set; }
    }
}
