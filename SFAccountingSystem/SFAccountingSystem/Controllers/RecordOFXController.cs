using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFAccountingSystem.Core.Enums;
using SFAccountingSystem.Core.Services;
using SFAccountingSystem.Core.ViewModels;

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
            ViewBag.Banks = new SelectList(Enum.GetValues(typeof(RecordOFXBank))
                                                .Cast<RecordOFXBank>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<RecordOFXBank>(),
                                                    Id = (int)x
                                                }), "Id", "DisplayName");


            return View(await _recordOFXService.List());
        }

        [HttpPost]
        public async Task<IActionResult> Index(ObjectFileViewModel model)
        {
            ViewBag.Banks = new SelectList(Enum.GetValues(typeof(RecordOFXBank))
                                    .Cast<RecordOFXBank>()
                                    .Select(x => new
                                    {
                                        DisplayName = x.ToName<RecordOFXBank>(),
                                        Id = (int)x
                                    }), "Id", "DisplayName");

            await _recordOFXService.Add(model.Ofx, model.Bank);

            return View(await _recordOFXService.List());
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            await _recordOFXService.Approve(id);
            return RedirectToAction("Index");
        }
    }
}
