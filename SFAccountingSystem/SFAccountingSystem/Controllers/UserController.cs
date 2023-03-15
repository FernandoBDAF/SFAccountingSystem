using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Enums;
using SFAccountingSystem.Models;
using SFAccountingSystem.ViewMoldes;

namespace SFAccountingSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly DataContext _dataContext;

        public UserController(ILogger<HomeController> logger, DataContext dataContext) //todos precisam ter logger, pra que serve? data context so  injeto onde for acessar o data bese ne?
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _dataContext.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Create() //tive que criar a viewbag, pois se colocar um objeto aqui diferente do post method da pau
        {
			SelectList select1 = new SelectList(Enum.GetValues(typeof(UserEntity))
												.Cast<UserEntity>()
												.Select(x => new
												{
													DisplayName = x.ToName<UserEntity>(),
													Id = (int)x
												}), "Id", "DisplayName");

			SelectList select2 = new SelectList(Enum.GetValues(typeof(UserType))
												.Cast<UserType>()
												.Select(x => new
												{
													DisplayName = x.ToName<UserType>(),
													Id = (int)x
												}), "Id", "DisplayName");
			ViewBag.Select = new UserSelectEnum
			{
				Select1 = select1,
				Select2 = select2
			};

			return View();
        }

        [HttpPost]
		public async Task<IActionResult> Create(User user) //importante fazer assync e revisar tudo que tem que ser assync
		{
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            await _dataContext.Users.AddAsync(user); //coloco await aqui tambem?
            await _dataContext.SaveChangesAsync();
			return RedirectToAction("Index");
		}

        public async Task<IActionResult> Update(int Id) // se eu colocar User user entra em conflito com o post
        {
            SelectList select1 = new SelectList(Enum.GetValues(typeof(UserEntity))
                                                .Cast<UserEntity>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<UserEntity>(),
                                                    Id = (int)x
                                                }), "Id", "DisplayName");

            SelectList select2 = new SelectList(Enum.GetValues(typeof(UserType))
                                                .Cast<UserType>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<UserType>(),
                                                    Id = (int)x
                                                }), "Id", "DisplayName");
            ViewBag.Select = new UserSelectEnum
            {
                Select1 = select1,
                Select2 = select2
            };

            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            
            if (user == null)
            {
                throw new Exception("There is not a user with this Id");
            }

            return View(user);
        }
        
        [HttpPost]
		public async Task<IActionResult> Update(User user) // quando devolvi nao informei o ID na volta, ele aproveita o que
                                                           // ja foi na ida ou tenho que ver todas as variaveis, inclusive as herdadas?
		{
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            else if (ModelState.IsValid)
            {
                _dataContext.Users.Update(user); //nao achei assync
                await _dataContext.SaveChangesAsync();
            }

            return RedirectToAction("Index"); //tentei colocar dentro do else if mas xiou
        }

		[HttpPost]
		public async Task<IActionResult> Delete(int Id)
		{
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == Id); //FirstAsync or FirstOrDefaultAsync?

            if (user == null)
            {
                throw new Exception("There is not a user with this Id");
            }

            else if (user != null)
            {
                _dataContext.Users.Remove(user); //nao achei assync
                await _dataContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
	}
}
