using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Core.Models;

namespace SFAccountingSystem.Core.Services
{
    public class RecordOFXService : BaseService<RecordOFX>
    {
        private readonly OFXService _ofxService;

        public RecordOFXService(DataContext context, OFXService oFXService) : base(context)
        {
            _ofxService = oFXService;
        }

        public async Task Add(IFormFile file)
        {
            foreach (var transaction in _ofxService.Process(file))
            {
                await context.RecordOFX.AddAsync(new RecordOFX(transaction));
            }

            await context.SaveChangesAsync();
        }

        public override async Task<List<RecordOFX>> List()
        {
            return await context.RecordOFX
                                .OrderByDescending(x => x.CreatedAt)
                                .ThenByDescending(x => x.Value)
                                .ToListAsync();
        }
    }
}
