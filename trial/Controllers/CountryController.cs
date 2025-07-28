using Microsoft.AspNetCore.Mvc;
using trial.Models;
using trial.Repositry.Base;

namespace trial.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ITripsRepository _tripRepository;

        public CountryController(ICountryRepository countryRepository, ITripsRepository tripRepository)
        {
            _countryRepository = countryRepository;
            _tripRepository = tripRepository;
        }

        // Get Request
        public IActionResult Index()
        {
            var countries = _countryRepository.FindAll();
            return View(countries);
        }

        //Get Request 
        public IActionResult New()
        {
            return View();
        }

        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Country country)
        {
            if (_countryRepository.CountryNameExists(country.CountryName))
            {
                ModelState.AddModelError("CountryName", "A country with this name already exists.");
                return View(country);
            }
            if (ModelState.IsValid)
            {
                if (country.clientFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    country.clientFile.CopyTo(stream);
                    country.dbImage = stream.ToArray();
                }
                _countryRepository.Addone(country);
                TempData["successData"] = "Country has been Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(country);
            }
        }

        //Get Request 
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var country = _countryRepository.FindById(Id.Value);

            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                var existingCountry = _countryRepository.FindById(country.Id);
                if (existingCountry != null)
                {
                    existingCountry.CountryName = country.CountryName;

                    if (country.clientFile != null)
                    {
                        MemoryStream stream = new MemoryStream();
                        country.clientFile.CopyTo(stream);
                        existingCountry.dbImage = stream.ToArray();
                    }

                    _countryRepository.UpdateOne(existingCountry);
                    TempData["successData"] = "Country has been updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return View(country);
            }
        }

        //Get Request 
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var country = _countryRepository.FindById(Id.Value);

            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Country country)
        {
            var trips = _tripRepository.FindAll().Where(t => t.CountryId == country.Id).ToList();
            foreach (var trip in trips)
            {
                _tripRepository.DeleteOne(trip);
            }

            _countryRepository.DeleteOne(country);
            TempData["successData"] = "Country and all associated trips have been deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
