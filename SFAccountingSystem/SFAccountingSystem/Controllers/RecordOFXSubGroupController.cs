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

        private readonly DataContext _dataContext;

        public RecordOFXSubGroupController(ILogger<RecordOFXSubGroupController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }



        public async Task<IActionResult> Index()
        {
            Dictionary<string, int> groups = new();

            foreach (var name in Enum.GetNames(typeof(RecordOFXGroup)))
            {
                int value = (int)Enum.Parse(typeof(RecordOFXGroup), name);
                groups.Add(name, value);
            }

            ViewBag.Groups = groups;

            var subgroups = await _dataContext.RecordOFXSubGroups.ToListAsync();
            return View(subgroups);
        }

        public async Task<IActionResult> IndexSubGroup(int id)
        {
            ViewBag.Group = id;

            var subgroups = await _dataContext.RecordOFXSubGroups
                                              .Include(x => x.ChildRecordsOFXes)
                                              .Include(x => x.RecordsOFXes)
                                              .Where(x => (int)x.Group == id)
                                              .ToListAsync();

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
                                                  .Include(x => x.ChildRecordsOFXes)
                                                  .Include(x => x.RecordsOFXes)
                                                  .Where(x => x.Group == subgroup.Group)
                                                  .Include(x => x.Parent)
                                                  .ToListAsync();

                return View(childrens);
            }

            return View();
        }


        public IActionResult Create(int id)
        {
            var list = Enum.GetValues(typeof(RecordOFXGroup))
                                                .Cast<RecordOFXGroup>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<RecordOFXGroup>(),
                                                    Id = (int)x
                                                });

            ViewBag.Groups = new SelectList(list, "Id", "DisplayName");

            return View(new RecordOFXSubGroup //recebo o id, mas ja envio um objeto com esse id
            {
                Group = (RecordOFXGroup)id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecordOFXSubGroup subgroup)
        {
            subgroup.Id = 0; // lucas vai checar, mas aqui eh pq esta sendo passado o id da rota

            if (!ModelState.IsValid)
            {
                return View(subgroup);
            }

            await _dataContext.RecordOFXSubGroups.AddAsync(subgroup);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("IndexSubGroup", new { id = (int)subgroup.Group });
        }

        public async Task<IActionResult> CreateSubChildren(int id)
        {
            var list = Enum.GetValues(typeof(RecordOFXGroup))
                                                .Cast<RecordOFXGroup>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<RecordOFXGroup>(),
                                                    Id = (int)x
                                                });

            ViewBag.Groups = new SelectList(list, "Id", "DisplayName");

            var parent = await _dataContext.RecordOFXSubGroups.FirstOrDefaultAsync(x => x.Id == id);

            var group = (RecordOFXGroup)0;

            if (parent != null)
            {
                group = parent.Group;
            }

            return View(new RecordOFXSubGroup
            {
                Group = group,
                ParentId = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubChildren(RecordOFXSubGroup subgroup)
        {
            subgroup.Id = 0;

            if (!ModelState.IsValid)
            {
                return View(subgroup);
            }

            await _dataContext.RecordOFXSubGroups.AddAsync(subgroup);
            await _dataContext.SaveChangesAsync();

            if (subgroup.ParentId != null)
            {
                return RedirectToAction("IndexChildrens", new { id = (int)subgroup.ParentId });
            }

            throw new NotImplementedException();
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
            if (!ModelState.IsValid)
            {
                return View(subGroup);
            }
            else if (ModelState.IsValid)
            {
                var current = await _dataContext.RecordOFXSubGroups.FirstOrDefaultAsync(x => x.Id == subGroup.Id);

                if (current != null)
                {
                    current.Description = subGroup.Description;

                    _dataContext.RecordOFXSubGroups.Update(current);
                    await _dataContext.SaveChangesAsync();

                    if (current.ParentId == null)
                    {
                        return RedirectToAction("IndexSubGroup", new { id = (int)current.Group });
                    }
                    else
                    {
                        return RedirectToAction("IndexChildrens", new { id = (int)current.ParentId });
                    }
                }
            }

            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(int id) //se tentar apagar um sub que tem filhos da erro, teria que tratar esse erro
        {
            var subgroup = await _dataContext.RecordOFXSubGroups.FirstOrDefaultAsync(x => x.Id == id);

            if (subgroup == null)
            {
                throw new Exception("There is not a subgroup with this Id");
            }
            else if (subgroup != null)
            {
                var group = subgroup.Group;

                _dataContext.RecordOFXSubGroups.Remove(subgroup);
                await _dataContext.SaveChangesAsync();

                if (subgroup.ParentId == null)
                {
                    return RedirectToAction("IndexSubGroup", new { id = (int)group });
                }
                else
                {
                    return RedirectToAction("IndexChildrens", new { id = (int)subgroup.ParentId });
                }
            }

            throw new NotImplementedException();
        }
    }
}
