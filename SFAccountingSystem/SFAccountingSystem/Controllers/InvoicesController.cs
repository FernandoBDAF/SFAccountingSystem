using Microsoft.AspNetCore.Mvc;
using SFAccountingSystem.Core.Services;

namespace SFAccountingSystem.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoicesService _invoiceService;

        public InvoicesController(InvoicesService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _invoiceService.List());
        }
    }
}
