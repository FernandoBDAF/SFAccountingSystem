using Microsoft.AspNetCore.Mvc;
using SFAccountingSystem.Core.Services;
using SFAccountingSystem.Core.ViewModels;

namespace SFAccountingSystem.Controllers
{
    public class BalanceSheetController : Controller
    {
        private BalanceSheetService _balanceSheetService;

        public BalanceSheetController(BalanceSheetService balanceSheetService)
        {
            _balanceSheetService = balanceSheetService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Incomes = await _balanceSheetService.GetIncomes();
            ViewBag.Expenses = await _balanceSheetService.GetExpenses();

            return View();
        }
    }
}
