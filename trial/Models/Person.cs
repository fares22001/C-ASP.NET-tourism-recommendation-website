using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace trial.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name cannot be longer than 255 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [Phone(ErrorMessage = "Invalid Mobile Number")]
        [StringLength(15, ErrorMessage = "Mobile number cannot be longer than 15 digits")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = "User"; // Default role

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        public void SetPassword(string password)
        {
            Password = HashPassword(password);
        }

        public ICollection<DBinterest> DBinterests { get; set; } = new List<DBinterest>();
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}