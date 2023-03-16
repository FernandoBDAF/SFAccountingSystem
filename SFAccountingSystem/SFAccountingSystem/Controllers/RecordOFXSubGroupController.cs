using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Enums;
using SFAccountingSystem.Models;
using SFAccountingSystem.ViewMoldes;
using System.Collections.Generic;

namespace SFAccountingSystem.Controllers
{
    public class RecordOFXSubGroupController : Controller
    {
        private readonly ILogger<RecordOFXSubGroupController> _logger;

        public readonly DataContext _dataContext;

        public RecordOFXSubGroupController(ILogger<RecordOFXSubGroupController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            Dictionary<string, int> groups = new Dictionary<string, int>();

            foreach (var name in Enum.GetNames(typeof(RecordOFXGroup)))
            {
                int value = (int)Enum.Parse(typeof(RecordOFXGroup), name);
                groups.Add(name, value);
            }

            ViewBag.Groups = groups;

            var subgroups = await _dataContext.RecordOFXSubGroups.ToListAsync();
            return View(subgroups);
        }

        public async Task<IActionResult> IndexSubGroup(int id) // se eu passar int group da erro
        {
            var subgroups = await _dataContext.RecordOFXSubGroups
                .Where(x => (int)x.Group == id).ToListAsync();
            if (subgroups != null) //mesmo quando eh null redireciona para outro lugar
            {
                return View(subgroups);
            }
            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> IndexChildrens(int id)
        {
            var subgroup = await _dataContext.RecordOFXSubGroups.FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.Subgroup = subgroup;
            if (subgroup != null)
            {
                var childrens = await _dataContext.RecordOFXSubGroups
                .Where(x => x.Group == subgroup.Group).ToListAsync();
                return View(childrens);
            }
            
            return View();
        }

        //o que fazer se eu quiser instanciar um enum manualmente?
        public IActionResult Create() //ja queria passar aqui o ID do enum
        {
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

        [HttpPost]
        public async Task<IActionResult> Create(RecordOFXSubGroup subgroup)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(subgroup);
            //}

            await _dataContext.RecordOFXSubGroups.AddAsync(subgroup);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("IndexSubGroup",  (int)subgroup.Group); // nao esta redirecionando correto
        }

        public IActionResult CreateSubChildren() //teria que passar o parentID ja fixo
        {
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

        [HttpPost]
        public async Task<IActionResult> CreateSubChildren(RecordOFXSubGroup subgroup)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(subgroup);
            //}

            await _dataContext.RecordOFXSubGroups.AddAsync(subgroup);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int Id)
        {
            var list = Enum.GetValues(typeof(RecordOFXGroup))
                                                .Cast<RecordOFXGroup>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<RecordOFXGroup>(),
                                                    Id = (int)x
                                                });

            ViewBag.Groups = new SelectList(list, "Id", "DisplayName");

            var subgroup = await _dataContext.RecordOFXSubGroups.FirstOrDefaultAsync(x => x.Id == Id);

            if (subgroup == null)
            {
                throw new Exception("There is not a subgroup with this Id");
            }

            return View(subgroup);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RecordOFXSubGroup subGroup)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(subGroup);
            //}
            //else if (ModelState.IsValid)
            //{
            var current = await _dataContext.RecordOFXSubGroups.FirstOrDefaultAsync(x => x.Id == subGroup.Id);

            if (current != null)
            {
                current.Group = subGroup.Group;
                current.Description = subGroup.Description;
                current.ParentId = subGroup.ParentId;

                _dataContext.RecordOFXSubGroups.Update(current);
                await _dataContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            //}

            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subgroup = await _dataContext.RecordOFXSubGroups.FirstOrDefaultAsync(x => x.Id == id);

            if (subgroup == null)
            {
                throw new Exception("There is not a subgroup with this Id");
            }

            else if (subgroup != null)
            {
                _dataContext.RecordOFXSubGroups.Remove(subgroup);
                await _dataContext.SaveChangesAsync();
            }

            return RedirectToAction("Index"); //tem como redirecionar pra pagina de onde veio?
        }
    }
}
