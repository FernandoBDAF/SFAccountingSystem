using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFAccountingSystem.Core.Enums;
using SFAccountingSystem.Core.Models;
using SFAccountingSystem.Core.Services;
using SFAccountingSystem.Core.ViewModels;

namespace SFAccountingSystem.Controllers
{
    public class RecordOFXController : Controller
    {
        private readonly RecordOFXService _recordOFXService;
        private readonly UserService _userService;

        public RecordOFXController(RecordOFXService recordOFXService,
                                    UserService userService)
        {
            _recordOFXService = recordOFXService;
            _userService = userService;
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

            ViewBag.Groups = new SelectList(Enum.GetValues(typeof(RecordOFXGroup))
                                                .Cast<RecordOFXGroup>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<RecordOFXGroup>(),
                                                    Id = (int)x
                                                }), "Id", "DisplayName");

            ViewBag.Users = new SelectList(await _userService.List(), "Id", "Name");


            return View(await _recordOFXService.List());
        }

        [HttpPost]
        public async Task<IActionResult> Index(ObjectFileViewModel model)
        {
            await _recordOFXService.Add(model.Ofx, model.Bank);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            await _recordOFXService.Approve(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGroup(RecordOFX model)
        {
            await _recordOFXService.UpdateGroup(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(RecordOFX model)
        {
            await _recordOFXService.UpdateUser(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubGroup(RecordOFX model)
        {
            await _recordOFXService.UpdateSubGroup(model);
            return RedirectToAction("Index");
        }
    }
}
