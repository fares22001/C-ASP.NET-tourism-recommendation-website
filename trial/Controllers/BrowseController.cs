using Microsoft.AspNetCore.Mvc;
using trial.Models;
using trial.Repositry.Base;
using System.Linq;
using System.Collections.Generic;

namespace trial.Controllers
{
    public class BrowseController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ITripsRepository _tripRepository;
        private readonly ICommentRepository _commentRepository;

        public BrowseController(ICountryRepository countryRepository, ITripsRepository tripRepository, ICommentRepository commentRepository)
        {
            _countryRepository = countryRepository;
            _tripRepository = tripRepository;
            _commentRepository = commentRepository;
        }

        public IActionResult Countries()
        {
            var countries = _countryRepository.FindAll();
            return View(countries);
        }

        public IActionResult CountryTrips(int? countryId)
        {
            if (countryId == null || countryId == 0)
            {
                return NotFound();
            }

            var country = _countryRepository.FindById(countryId.Value);
            if (country == null)
            {
                return NotFound();
            }

            var trips = _tripRepository.FindAll().Where(t => t.CountryId == countryId.Value);
            ViewBag.CountryName = country.CountryName;
            return View(trips);
        }

        public IActionResult TripDetails(int? tripId)
        {
            if (tripId == null || tripId == 0)
            {
                return NotFound();
            }

            var trip = _tripRepository.FindById(tripId.Value);
            if (trip == null)
            {
                return NotFound();
            }

            var comments = _commentRepository.GetCommentsByTripId(tripId.Value);
            ViewBag.Comments = comments ?? new List<Comment>(); // Ensure it's not null
            return View(trip);
        }
    }
}
