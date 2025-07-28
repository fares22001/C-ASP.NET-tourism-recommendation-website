using Microsoft.AspNetCore.Mvc;
using trial.Models;
using trial.Repositry.Base;

namespace trial.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public AdminController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IActionResult Index()
        {
            var persons = _personRepository.FindAll();
            return View(persons);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                person.SetPassword(person.Password); // Hash the password before saving
                _personRepository.Addone(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public IActionResult Edit(int id)
        {
            var person = _personRepository.FindById(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingPerson = _personRepository.FindById(id);
                if (existingPerson == null)
                {
                    return NotFound();
                }

                existingPerson.Name = person.Name;
                existingPerson.Email = person.Email;
                existingPerson.MobileNumber = person.MobileNumber;
                existingPerson.Role = person.Role;

                if (!string.IsNullOrEmpty(person.Password))
                {
                    existingPerson.SetPassword(person.Password); // Hash the new password if provided
                }

                _personRepository.UpdateOne(existingPerson);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public IActionResult Delete(int id)
        {
            var person = _personRepository.FindById(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var person = _personRepository.FindById(id);
            if (person == null)
            {
                return NotFound();
            }
            _personRepository.DeleteOne(person);
            return RedirectToAction(nameof(Index));
        }
    }
}
