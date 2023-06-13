using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFAccountingSystem.Core.Models;
using SFAccountingSystem.Core.Services;

namespace SFAccountingSystem.Controllers
{
    public class IntermediationsController : Controller
    {
        private readonly UserService _userService;
        private readonly IntermediationsService _intermediationsService;

        public IntermediationsController(UserService userService,
                                         IntermediationsService intermediationsService)
        {
            _userService = userService;
            _intermediationsService = intermediationsService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _intermediationsService.List());
        }

        public async Task<IActionResult> New()
        {
            ViewBag.Users = new SelectList(await _userService.List(), "Id", "Name");
            ViewBag.RecordOfxs = await _intermediationsService.GetNotBounded();

            return View(new Intermediation());
        }

        [HttpPost]
        public async Task<IActionResult> New(Intermediation model)
        {
            await _intermediationsService.Add(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> View(int id)
        {
            ViewBag.Users = new SelectList(await _userService.List(), "Id", "Name");
            return View(await _intermediationsService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> View(int id, Intermediation model)
        {
            await _intermediationsService.CreateInvoice(model);
            return RedirectToAction("Index", "Invoices");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _intermediationsService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
