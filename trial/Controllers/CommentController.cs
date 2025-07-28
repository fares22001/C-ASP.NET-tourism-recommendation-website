using Microsoft.AspNetCore.Mvc;
using trial.Models;
using trial.Repositry.Base;

namespace trial.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ITripsRepository _tripRepository;

        public CommentController(ICommentRepository commentRepository, IPersonRepository personRepository, ITripsRepository tripRepository)
        {
            _commentRepository = commentRepository;
            _personRepository = personRepository;
            _tripRepository = tripRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(int tripId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                ModelState.AddModelError(string.Empty, "Comment content cannot be empty.");
                return RedirectToAction("TripDetails", "Browse", new { tripId });
            }

            var userEmail = HttpContext.Session.GetString("PersonEmail");
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Person");
            }

            var person = _personRepository.FindAll().FirstOrDefault(p => p.Email == userEmail);
            if (person == null)
            {
                return RedirectToAction("Login", "Person");
            }

            var trip = _tripRepository.FindById(tripId);
            if (trip == null)
            {
                return NotFound();
            }

            var comment = new Comment
            {
                Content = content,
                PersonId = person.Id,
                TripId = tripId
            };

            _commentRepository.Addone(comment);
            return RedirectToAction("TripDetails", "Browse", new { tripId });
        }
    }
}
