using Microsoft.AspNetCore.Mvc;
using trial.Models;
using trial.Repositry.Base;

namespace trial.Controllers
{
    public class AdmindashboardController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICommentRepository _commentRepository;

        public AdmindashboardController(IPersonRepository personRepository, ICountryRepository countryRepository, ICommentRepository commentRepository)
        {
            _personRepository = personRepository;
            _countryRepository = countryRepository;
            _commentRepository = commentRepository;
        }

        public IActionResult Index()
        {
            var userCount = _personRepository.FindAll().Count();
            var countryCount = _countryRepository.FindAll().Count();
            var reviewCount = _commentRepository.FindAll().Count();

            ViewBag.UserCount = userCount;
            ViewBag.CountryCount = countryCount;
            ViewBag.ReviewCount = reviewCount;

            return View();
        }
    }
}