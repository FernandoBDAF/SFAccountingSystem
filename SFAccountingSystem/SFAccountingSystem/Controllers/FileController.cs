using Microsoft.AspNetCore.Mvc;
using SFAccountingSystem.Services;
using SFAccountingSystem.ViewMoldes;

namespace SFAccountingSystem.Controllers
{
    public class FileController : Controller
    {
        private readonly OfxService _ofxService;

        public FileController(OfxService ofxService)
        {
            _ofxService = ofxService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ObjectFileViewModel model)
        {

            ViewBag.Rows = _ofxService.Process(model.Ofx);
            return View();
        }
    }
}