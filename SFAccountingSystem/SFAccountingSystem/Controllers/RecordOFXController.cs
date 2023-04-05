using Microsoft.AspNetCore.Mvc;
using SFAccountingSystem.Services;
using SFAccountingSystem.ViewMoldes;

namespace SFAccountingSystem.Controllers
{
    public class RecordOFXController : Controller
    {
        private readonly RecordOFXService _recordOFXService;

        public RecordOFXController(RecordOFXService recordOFXService)
        {
            _recordOFXService = recordOFXService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _recordOFXService.List());
        }

        [HttpPost]
        public async Task<IActionResult> Index(ObjectFileViewModel model)
        {
            await _recordOFXService.Add(model.Ofx);

            return View(await _recordOFXService.List());
        }
    }
}
