using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using trial.Models;
using trial.Repositry.Base;

namespace trial.Controllers
{
    public class InterestController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IGenericRepository<DBinterest> _interestRepository;
        

        public InterestController(ICountryRepository countryRepository, IPersonRepository personRepository, IGenericRepository<DBinterest> interestRepository)
        {
            _countryRepository = countryRepository;
            _personRepository = personRepository;
            _interestRepository = interestRepository;
           
        }

        // Action to display the form
        public IActionResult AddInterest()
        {
            var countries = _countryRepository.FindAll();
            return View(countries);
        }

        public IActionResult Index()
        {
            var interest = _interestRepository.FindAll();
            return View(interest);
        }

        public IActionResult DeleteInterest(int id)
        {
            var interest = _interestRepository.FindById(id);
            if (interest != null)
            {
                _interestRepository.DeleteOne(interest);
            }
            return RedirectToAction("Index");
        }

        // Action to save the interest
        [HttpPost]
        public IActionResult SaveInterest(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("Name", "Interest name is required");
            }
            var userEmail = HttpContext.Session.GetString("PersonEmail");

            var personId = _personRepository.FindAll().FirstOrDefault(p => p.Email == userEmail);
            if (personId == null)
            {
                ModelState.AddModelError("PersonId", "User is not logged in");
                return RedirectToAction("AddInterest");
            }

            if (ModelState.IsValid)
            {
                var dbInterest = new DBinterest
                {
                    Name = name,
                    PersonId = personId.Id
                };

                _interestRepository.Addone(dbInterest);
                return RedirectToAction("AddInterest");
            }

            var countries = _countryRepository.FindAll();
            return View("AddInterest", countries);
        }
    }
}
