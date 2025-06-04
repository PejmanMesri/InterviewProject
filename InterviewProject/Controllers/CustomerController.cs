using InterviewProject.Entities;
using InterviewProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InterviewProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _repository.GetAllAsync().ConfigureAwait(true);
            return View(customers);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Id = Guid.NewGuid();
                await _repository.AddAsync(customer).ConfigureAwait(true);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(id).ConfigureAwait(true);
                return View(customer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(customer).ConfigureAwait(true);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(id).ConfigureAwait(true);
                return View(customer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _repository.DeleteAsync(id).ConfigureAwait(true);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return RedirectToAction(nameof(Index));
            }

            var results = await _repository.SearchByNameAsync(name).ConfigureAwait(true);
            return View("Index", results);
        }
    }
}