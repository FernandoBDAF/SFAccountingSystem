using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Core.Enums;
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

        public async Task Add(IFormFile file, RecordOFXBank bank)
        {
            foreach (var transaction in _ofxService.Process(file))
            {
                if (await context.RecordOFX.AnyAsync(x => x.Value == transaction.Value
                                                          && x.Date == transaction.Date.Date
                                                          && x.Bank == bank
                                                          && x.FITID == transaction.FITID))
                {
                    continue;
                }

                await context.RecordOFX.AddAsync(new RecordOFX(transaction, bank));
            }

            await context.SaveChangesAsync();
        }

        public override async Task<List<RecordOFX>> List()
        {
            return await context.RecordOFX
                                .OrderBy(x => x.Date)
                                .ThenByDescending(x => x.Value)
                                .ToListAsync();
        }
    }
}
