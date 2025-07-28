using Microsoft.AspNetCore.Mvc;
using trial.Models;
using trial.Repositry.Base;
using System.Linq;

namespace trial.Controllers
{
    public class BookController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITripsRepository _tripsRepository;
        private readonly IGenericRepository<Book> _bookRepository;

        public BookController(IPersonRepository personRepository, ITripsRepository tripsRepository, IGenericRepository<Book> bookRepository)
        {
            _personRepository = personRepository;
            _tripsRepository = tripsRepository;
            _bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            var books = _bookRepository.FindAll(b => b.Trip);
            return View(books);
        }

        [HttpPost]
        public IActionResult AddBook(int tripId)
        {
            var userEmail = HttpContext.Session.GetString("PersonEmail");

            var personId = _personRepository.FindAll().FirstOrDefault(p => p.Email == userEmail);
            if (personId == null)
            {
                ModelState.AddModelError("PersonId", "User is not logged in");
                return RedirectToAction("TripDetails");
            }

            if (ModelState.IsValid)
            {
                var newBook = new Book
                {
                    TripId = tripId,
                    PersonId = personId.Id
                };

                _bookRepository.Addone(newBook);
                return RedirectToAction("Index");
            }

            return View("TripDetails");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var book = _bookRepository.FindById(id);
            if (book != null)
            {
                _bookRepository.DeleteOne(book);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
