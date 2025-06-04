using InterviewProject.Entities;
using InterviewProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InterviewProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;

        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _repository.GetOrdersWithCommissionsAsync().ConfigureAwait(true);
            return View(orders);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var order = await _repository.GetOrderWithFullDetailsAsync(id).ConfigureAwait(true);
                return View(order);
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
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                await _repository.AddAsync(order).ConfigureAwait(true);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var order = await _repository.GetByIdAsync(id).ConfigureAwait(true);
                return View(order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(order).ConfigureAwait(true);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var order = await _repository.GetByIdAsync(id).ConfigureAwait(true);
                return View(order);
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

        public async Task<IActionResult> FindByOrderNo(int orderNo)
        {
            try
            {
                var order = await _repository.GetByOrderNoAsync(orderNo).ConfigureAwait(true);
                return View("Details", order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}