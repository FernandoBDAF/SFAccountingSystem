using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFAccountingSystem.Enums;

namespace SFAccountingSystem.Controllers
{
    public class RecordOFXSubGroupController : Controller
    {
        public IActionResult Index()
        {
            //nao entendi onde o enum helper foi chamado

            var list = Enum.GetValues(typeof(RecordOFXGroup))
                                                .Cast<RecordOFXGroup>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<RecordOFXGroup>(),
                                                    Id = (int)x
                                                });

            ViewBag.Groups = new SelectList(list, "Id", "DisplayName");


            return View();
        }
    }
}
