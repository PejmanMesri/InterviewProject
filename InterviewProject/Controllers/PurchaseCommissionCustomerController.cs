using InterviewProject.Entities;
using InterviewProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InterviewProject.Controllers
{
    public class PurchaseCommissionCustomerController : Controller
    {
        private readonly IPurchaseCommissionCustomerRepository _repository;

        public PurchaseCommissionCustomerController(IPurchaseCommissionCustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _repository.GetWithDetailsAsync().ConfigureAwait(true);
            return View(items);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id).ConfigureAwait(true);
                return View(item);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseCommissionCustomer item)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(item).ConfigureAwait(true);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id).ConfigureAwait(true);
                return View(item);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PurchaseCommissionCustomer item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(item).ConfigureAwait(true);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id).ConfigureAwait(true);
                return View(item);
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

        //public async Task<IActionResult> BySeller(Guid sellerId)
        //{
        //    var items = await _repository.GetBySellerIdAsync(sellerId);
        //    return View("Index", items);
        //}

        public async Task<IActionResult> ByCommission(Guid commissionId)
        {
            var items = await _repository.GetByPurchaseCommissionIdAsync(commissionId).ConfigureAwait(true);
            return View("Index", items);
        }
    }
}