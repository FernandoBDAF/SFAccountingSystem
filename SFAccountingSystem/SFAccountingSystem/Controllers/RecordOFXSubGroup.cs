using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFAccountingSystem.Enums;

namespace SFAccountingSystem.Controllers
{
    public class RecordOFXSubGroup : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Groups = new SelectList(Enum.GetValues(typeof(RecordOFXGroup))
                                                .Cast<RecordOFXGroup>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<RecordOFXGroup>(),
                                                    Id = (int)x
                                                }), "Id", "DisplayName");


            return View();
        }
    }
}
