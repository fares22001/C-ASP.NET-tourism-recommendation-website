using Microsoft.AspNetCore.Mvc;
using trial.Models;
using trial.Repositry.Base;

namespace trial.Controllers
{
    public class MenuController : Controller
    {
        private readonly ITripsRepository _repository;

        public MenuController(ITripsRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult gettrips()
        {
            var trips = _repository.FindAll();
            return View(trips);
        }
    }
}
