using DapperMvcDemo.Data.Models.Domain;
using DapperMvcDemo.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DapperMvcDemo.UI.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepo;

        public PersonController(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }
        

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(person);

                bool addPersonResult = await _personRepo.AddAsync(person);

                if (addPersonResult)
                {
                    TempData["msg"] = "Person added successfully.";
                    ModelState.Clear();
                    return RedirectToAction(nameof(Add));
                }
                else
                {
                    TempData["msg"] = "Could not add person.";
                }
            }
            catch (Exception)
            {
                TempData["msg"] = "An error occurred while adding the person.";
            }

            return View(person);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personRepo.GetByIdAsync(id);
            if (person == null)
                return NotFound();

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(person);

                var updateResult = await _personRepo.UpdateAsync(person);
                TempData["msg"] = updateResult ? "Person updated successfully." : "Could not update person.";
            }
            catch (Exception)
            {
                TempData["msg"] = "An error occurred while updating the person.";
            }

            return View(person);
        }

        public async Task<IActionResult> DisplayAll()
        {
            var people = await _personRepo.GetAllAsync();
            return View(people);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleteResult = await _personRepo.DeleteAsync(id);
                TempData["msg"] = deleteResult ? "Person deleted successfully." : "Could not delete person.";
            }
            catch (Exception)
            {
                TempData["msg"] = "An error occurred while deleting the person.";
            }

            return RedirectToAction(nameof(DisplayAll));
        }
    }
}
