using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace trial.Models
{
    public class Trips
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Place name")]
        public string tripName { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [Range(10, 5000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public decimal Price { get; set; }

        [NotMapped]
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

        [Required]
        [DisplayName("Country ID")]
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public Country? Country { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}