using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Enums;
using SFAccountingSystem.Models;

namespace SFAccountingSystem.Controllers
{
    public class RecordOFXSubGroupController : Controller
    {
        private ILogger<RecordOFXSubGroupController> _logger;
        private readonly DataContext _dataContext;

        public RecordOFXSubGroupController(DataContext dataContext, ILogger<RecordOFXSubGroupController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index(RecordOFXGroup? groupId = null, int? parentId = null)
        {
            ViewData["SubTitle"] = " <a href='/RecordOFXSubGroup'>Groups</a>";

            if (groupId == null)
            {
                Dictionary<string, int> groups = new();

                foreach (var name in Enum.GetNames(typeof(RecordOFXGroup)))
                {
                    int value = (int)Enum.Parse(typeof(RecordOFXGroup), name);
                    groups.Add(name, value);
                }

                return View(groups);
            }
            else if (groupId.HasValue)
            {
                ViewBag.Group = groupId;
                ViewBag.ParentId = parentId;

                ViewData["SubTitle"] += $" > <a href='/RecordOFXSubGroup/?groupId={(int)groupId}'>{groupId.ToName<RecordOFXGroup>()}</a>";

                if (parentId.HasValue)
                {
                    await RecursiveMethod(parentId.Value);
                }

                return View("IndexSubGroup", await _dataContext.RecordOFXSubGroups
                                                               .Include(x => x.RecordsOFXes)
                                                               .Include(x => x.ChildRecordsOFXes)
                                                               .Where(x => x.Group == groupId && x.ParentId == parentId)
                                                               .ToListAsync());
            }

            throw new NotImplementedException();
        }

        public async Task<IActionResult> Create(RecordOFXGroup groupId, int? parentId = null)
        {
            await Task.Yield();

            ViewBag.Groups = new SelectList(Enum.GetValues(typeof(RecordOFXGroup))
                                                .Cast<RecordOFXGroup>()
                                                .Select(x => new
                                                {
                                                    DisplayName = x.ToName<RecordOFXGroup>(),
                                                    Id = (int)x
                                                }), "Id", "DisplayName");

            return View(new RecordOFXSubGroup
            {
                Group = groupId,
                ParentId = parentId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecordOFXSubGroup model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Groups = new SelectList(Enum.GetValues(typeof(RecordOFXGroup))
                                                        .Cast<RecordOFXGroup>()
                                                        .Select(x => new
                                                        {
                                                            DisplayName = x.ToName<RecordOFXGroup>(),
                                                            Id = (int)x
                                                        }), "Id", "DisplayName");

                    return View(model);
                }
                else if (ModelState.IsValid)
                {
                    await _dataContext.RecordOFXSubGroups.AddAsync(model);
                    await _dataContext.SaveChangesAsync();

                    return RedirectToAction("Index", new { parentId = model.ParentId, groupId = model.Group });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Groups = new SelectList(Enum.GetValues(typeof(RecordOFXGroup))
                                                    .Cast<RecordOFXGroup>()
                                                    .Select(x => new
                                                    {
                                                        DisplayName = x.ToName<RecordOFXGroup>(),
                                                        Id = (int)x
                                                    }), "Id", "DisplayName");
                ModelState.AddModelError("", ex.Message);

                return View(model);
            }

            throw new NotImplementedException();
        }


        private async Task RecursiveMethod(int currentId)
        {
            var current = await _dataContext.RecordOFXSubGroups.FirstOrDefaultAsync(x => x.Id == currentId);

            if (current != null)
            {
                if (current.ParentId.HasValue)
                {
                    await RecursiveMethod(current.ParentId.Value);
                }

                ViewData["SubTitle"] += $" > <a href='/RecordOFXSubGroup/?groupId={(int)current.Group}&parentId={current.Id}'>{current.Description}</a>";
            }
        }
    }
}