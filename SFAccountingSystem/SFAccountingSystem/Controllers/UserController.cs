using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Core;
using SFAccountingSystem.Core.Enums;
using SFAccountingSystem.Core.Models;

namespace SFAccountingSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public readonly DataContext _dataContext;

        public UserController(ILogger<UserController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _dataContext.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Create()
        {
            SelectList select1 = new(Enum.GetValues(typeof(UserEntity))
                                                .Cast<UserEntity>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<UserEntity>(),
                                                    Id = (int)x
                                                }), "Id", "DisplayName");

            SelectList select2 = new(Enum.GetValues(typeof(UserType))
                                                .Cast<UserType>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<UserType>(),
                                                    Id = (int)x
                                                }), "Id", "DisplayName");

            ViewBag.Select1 = select1;
            ViewBag.Select2 = select2;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int Id)
        {
            SelectList select1 = new(Enum.GetValues(typeof(UserEntity))
                                                .Cast<UserEntity>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<UserEntity>(),
                                                    Id = (int)x
                                                }), "Id", "DisplayName");

            SelectList select2 = new(Enum.GetValues(typeof(UserType))
                                                .Cast<UserType>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<UserType>(),
                                                    Id = (int)x
                                                }), "Id", "DisplayName");

            ViewBag.Select1 = select1;
            ViewBag.Select2 = select2;

            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == Id);

            if (user == null)
            {
                throw new Exception("There is not a user with this Id");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else if (ModelState.IsValid)
            {
                var current = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

                if (current != null)
                {
                    current.Name = user.Name;
                    current.CpfCnpj = user.CpfCnpj;
                    current.Type = user.Type;
                    current.Entity = user.Entity;

                    _dataContext.Users.Update(current);
                    await _dataContext.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }

            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == Id);

            if (user == null)
            {
                throw new Exception("There is not a user with this Id");
            }

            else if (user != null)
            {
                _dataContext.Users.Remove(user);
                await _dataContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
