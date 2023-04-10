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
            return await context.RecordOFXSubGroups
                                .Where(x => x.Group == groupId)
                                .OrderBy(x => x.ParentId)
                                .ToListAsync();
        }
    }
}
