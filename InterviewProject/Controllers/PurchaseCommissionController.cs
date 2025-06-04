using InterviewProject.Entities;
using InterviewProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InterviewProject.Controllers
{
    public class PurchaseCommissionController : Controller
    {
        private readonly IPurchaseCommissionRepository _repository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public PurchaseCommissionController(
            IPurchaseCommissionRepository repository,
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository)
        {
            _repository = repository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }


        //public async Task<IActionResult> Index()
        //{
        //    var commissions = await _repository.GetCommissionsWithCustomersAsync();
        //    return View(commissions);
        //}

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var commission = await _repository.GetByIdAsync(id).ConfigureAwait(true);
                return View(commission);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Create()
        {
            // Get orders and customers for dropdowns
            var orders = await _orderRepository.GetAllAsync().ConfigureAwait(true);
            var sellers = await _customerRepository.GetAllAsync().ConfigureAwait(true);

            ViewBag.Orders = new SelectList(orders, "Id", "OrderNo");
            ViewBag.Sellers = sellers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchaseCommission commission)
        {
           // if (ModelState.IsValid)
            //{
                await _repository.AddAsync(commission).ConfigureAwait(true);
                return RedirectToAction(nameof(Create));
           // }
            //return View(commission);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var commission = await _repository.GetByIdAsync(id).ConfigureAwait(true);
                return View(commission);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PurchaseCommission commission)
        {
            if (id != commission.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(commission).ConfigureAwait(true);
                return RedirectToAction(nameof(Index));
            }
            return View(commission);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var commission = await _repository.GetByIdAsync(id).ConfigureAwait(true);
                return View(commission);
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


    }
}