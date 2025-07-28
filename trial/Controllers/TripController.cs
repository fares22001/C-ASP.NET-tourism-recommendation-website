using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using trial.Models;
using trial.Repositry.Base;
using System.Linq;

namespace trial.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripsRepository _tripsRepository;
        private readonly ICountryRepository _countryRepository;

        public TripController(ITripsRepository tripsRepository, ICountryRepository countryRepository)
        {
            _tripsRepository = tripsRepository;
            _countryRepository = countryRepository;
        }

        // Get Request
        public IActionResult Index()
        {
            var trips = _tripsRepository.FindAllWithDetails();
            return View(trips);
        }

        // Get Request
        public IActionResult New()
        {
            ViewData["Title"] = "Add New Trip";
            ViewBag.Countries = _countryRepository.FindAll().Select(c => new SelectListItem { Text = c.CountryName, Value = c.Id.ToString() });
            return View();
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Trips trip)
        {
            if (ModelState.IsValid)
            {
                if (trip.clientFile != null && trip.clientFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        trip.clientFile.CopyTo(memoryStream);
                        trip.dbImage = memoryStream.ToArray();
                    }
                }

                _tripsRepository.Addone(trip);
                TempData["successData"] = "Trip has been created successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.Countries = _countryRepository.FindAll().Select(c => new SelectListItem { Text = c.CountryName, Value = c.Id.ToString() });
            return View(trip);
        }

        // Get Request
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var trip = _tripsRepository.FindById(Id.Value);

            if (trip == null)
            {
                return NotFound();
            }

            ViewBag.Countries = _countryRepository.FindAll().Select(c => new SelectListItem { Text = c.CountryName, Value = c.Id.ToString(), Selected = c.Id == trip.CountryId });
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Trips trip)
        {
            if (ModelState.IsValid)
            {
                var existingTrip = _tripsRepository.FindById(trip.Id);
                if (existingTrip != null)
                {
                    existingTrip.tripName = trip.tripName;
                    existingTrip.Description = trip.Description;
                    existingTrip.Price = trip.Price;
                    existingTrip.CountryId = trip.CountryId;

                    if (trip.clientFile != null && trip.clientFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            trip.clientFile.CopyTo(memoryStream);
                            existingTrip.dbImage = memoryStream.ToArray();
                        }
                    }

                    _tripsRepository.UpdateOne(existingTrip);
                    TempData["successData"] = "Trip has been updated successfully.";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Countries = _countryRepository.FindAll().Select(c => new SelectListItem { Text = c.CountryName, Value = c.Id.ToString(), Selected = c.Id == trip.CountryId });
            return View(trip);
        }

        // Get Request
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var trip = _tripsRepository.FindById(Id.Value);

            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Trips trip)
        {
            _tripsRepository.DeleteOne(trip);
            TempData["successData"] = "Trip has been deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
