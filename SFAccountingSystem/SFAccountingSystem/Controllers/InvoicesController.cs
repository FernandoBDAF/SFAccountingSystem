using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFAccountingSystem.Core.Models;
using SFAccountingSystem.Core.Services;

namespace SFAccountingSystem.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoicesService _invoiceService;
        private readonly UserService _userService;

        public InvoicesController(InvoicesService invoiceService,
                                  UserService userService)
        {
            _invoiceService = invoiceService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Users = new SelectList(await _userService.List(), "Id", "Name");

            return View(await _invoiceService.List());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(Invoice model)
        {
            await _invoiceService.UpdateUser(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNrNumber(Invoice model)
        {
            await _invoiceService.UpdateNrNumber(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateValue(Invoice model)
        {
            await _invoiceService.UpdateValue(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDate(Invoice model)
        {
            await _invoiceService.UpdateDate(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            await _invoiceService.Approve(id);
            return RedirectToAction("Index");
        }
    }
}
