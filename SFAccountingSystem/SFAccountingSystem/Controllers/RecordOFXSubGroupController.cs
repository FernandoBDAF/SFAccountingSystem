using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Enums;

namespace SFAccountingSystem.Controllers
{
    public class RecordOFXSubGroupController : Controller
    {
        private readonly DataContext _dataContext;

        public RecordOFXSubGroupController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index(RecordOFXGroup? groupId = null, int? parentId = null)
        {
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
                return View("IndexSubGroup", await _dataContext.RecordOFXSubGroups
                                                               .Include(x => x.RecordsOFXes)
                                                               .Include(x => x.ChildRecordsOFXes)
                                                               .Where(x => x.Group == groupId && x.ParentId == parentId)
                                                               .ToListAsync());
            }

            throw new NotImplementedException();
        }
    }
}