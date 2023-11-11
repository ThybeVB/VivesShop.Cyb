using Microsoft.AspNetCore.Mvc;
using PeopleManager.Cyb.Ui.Mvc.Core;
using PeopleManager.Cyb.Ui.Mvc.Models;

namespace PeopleManager.Cyb.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleManagerDbContext _dbContext;

        public PeopleController(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var people = _dbContext.People.ToList();
            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            _dbContext.People.Add(person);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var person = _dbContext.People.FirstOrDefault(p => p.Id == id);
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            var dbPerson = _dbContext.People.FirstOrDefault(p => p.Id == id);

            if (dbPerson == null)
            {
                return RedirectToAction("Index");
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute]int id)
        {
            var person = _dbContext.People.FirstOrDefault(p => p.Id == id);
            return View(person);
        }

        [HttpPost("/people/delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([FromRoute]int id)
        {
            var dbPerson = _dbContext.People.FirstOrDefault(p => p.Id == id);

            if (dbPerson is null)
            {
                return RedirectToAction("Index");
            }

            _dbContext.People.Remove(dbPerson);

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
