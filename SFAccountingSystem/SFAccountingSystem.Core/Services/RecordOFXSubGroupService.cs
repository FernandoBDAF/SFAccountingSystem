using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Core.Enums;
using SFAccountingSystem.Core.Models;

namespace SFAccountingSystem.Core.Services
{
    public class RecordOFXSubGroupService : BaseService<RecordOFXSubGroup>
    {
        public RecordOFXSubGroupService(DataContext context) : base(context)
        {
        }

        public async Task<List<RecordOFXSubGroup>> List(RecordOFXGroup groupId)
        {
            var list = new List<RecordOFXSubGroup>();


            var query = await context.RecordOFXSubGroups
                                        .Where(x => x.Group == groupId && !x.ParentId.HasValue)
                                        .OrderBy(x => x.ParentId)
                                        .ToListAsync();

            foreach (var subgroup in query)
            {
                list.Add(subgroup);
                list.AddRange(await GetChildren(subgroup.Id));
            }

            return list;
        }

        private async Task<List<RecordOFXSubGroup>> GetChildren(int subGroupId, int level = 1)
        {
            var list = new List<RecordOFXSubGroup>();
            var chd = await context.RecordOFXSubGroups
                                    .Where(x => x.ParentId == subGroupId)
                                    .ToListAsync();

            foreach (var item in chd)
            {
                var levelDescription = "";

                for (int i = 0; i < level; i++)
                {
                    levelDescription += "-";
                }

                list.Add(new RecordOFXSubGroup
                {
                    Id = item.Id,
                    Description = $"{levelDescription} {item.Description}"
                });

                list.AddRange(await GetChildren(item.Id, level + 1));
            }

            return list;
        }
    }
}
