using Microsoft.AspNetCore.Mvc;

namespace SFAccountingSystem.Controllers
{
    public class ExpensesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
