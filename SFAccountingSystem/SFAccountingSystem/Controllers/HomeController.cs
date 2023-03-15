using Microsoft.AspNetCore.Mvc;
using SFAccountingSystem.Models;
using System.Diagnostics;

namespace SFAccountingSystem.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger) //todos precisam ter logger, pra que serve? data context so  injeto onde for acessar o data base ne?
		{
			_logger = logger;
		}

		//public readonly DataContext _dataContext;
		//public HomeController(DataContext dataContext)
		//{
		//	_dataContext = dataContext;
		//}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}