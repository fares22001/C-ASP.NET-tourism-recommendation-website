using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using trial.Models;
using trial.Repositry.Base;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace trial.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IActionResult Register()
        {
            return View(new Person());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Person person)
        {
            if (ModelState.IsValid)
            {
                if (_personRepository.ExistsByEmail(person.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return View(person);
                }

                // Validation logic from old UserController
                if (string.IsNullOrWhiteSpace(person.Password) || IsNumericAndNonPositive(person.Password))
                {
                    ModelState.AddModelError("Password", "Password must not be null");
                    return View(person);
                }
                else if (person.Password.Length < 8)
                {
                    ModelState.AddModelError("Password", "Password must be 8 characters or more");
                    return View(person);
                }
                else if (!HasUppercase(person.Password))
                {
                    ModelState.AddModelError("Password", "Password must contain at least one uppercase");
                    return View(person);
                }
                else if (string.IsNullOrWhiteSpace(person.Email) || !IsValidEmail(person.Email) || IsNumericAndNonPositive(person.Email))
                {
                    ModelState.AddModelError("Email", "Invalid email format");
                    return View(person);
                }
                else if (string.IsNullOrWhiteSpace(person.Name) || IsNumericAndNonPositive(person.Name))
                {
                    ModelState.AddModelError("Name", "Invalid Name");
                    return View(person);
                }
                else if (person.MobileNumber.Length != 11)
                {
                    ModelState.AddModelError("MobileNumber", "Invalid phone number.Phone number must be 11 digits.");
                    return View(person);
                }

                // End of validation logic from old UserController

                person.SetPassword(person.Password); // Hash password before saving
                person.Role = "User"; // Default role set to 'User', can be changed based on logic or form input
                _personRepository.Addone(person);
                return RedirectToAction("Login"); // Redirect to login page after successful registration
            }
            return View(person);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            var person = _personRepository.FindAll().FirstOrDefault(p => p.Email == email && p.Password == p.HashPassword(password));
            if (person != null)
            {
                HttpContext.Session.SetString("PersonEmail", person.Email);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, person.Name),
                    new Claim(ClaimTypes.Email, person.Email),
                    new Claim(ClaimTypes.Role, person.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    RedirectUri = Url.Content("~/")  // Default redirect URI
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // Check the role and redirect accordingly
                if (person.Role == "Admin")
                {
                    HttpContext.Session.SetString("PersonEmail", person.Email);
                    return RedirectToAction("Index", "Admindashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Validation helper methods
        public static bool HasUppercase(string password)
        {
            return password.Any(char.IsUpper);
        }

        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool IsNumericAndNonPositive(string value)
        {
            // Check if value is numeric and non-positive
            return double.TryParse(value, out double result) && result <= 0;
        }
    }
}
