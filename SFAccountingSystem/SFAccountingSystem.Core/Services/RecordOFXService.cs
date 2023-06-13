using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Core.Enums;
using SFAccountingSystem.Core.Models;
using SFAccountingSystem.Core.ViewModels;
using System.Net.Mime;

namespace SFAccountingSystem.Core.Services
{
    public class RecordOFXService : BaseService<RecordOFX>
    {
        private readonly OFXService _ofxService;
        private readonly InvoicesService _invoicesService;

        public RecordOFXService(DataContext context,
                                OFXService oFXService,
                                InvoicesService invoicesService) : base(context)
        {
            _ofxService = oFXService;
            _invoicesService = invoicesService;
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

        public async Task Approve(int id)
        {
            var record = await context.RecordOFX.FirstOrDefaultAsync(x => x.Id == id);

            if (record == null)
                return;

            if (record.ApprovedAt.HasValue)
            {
                record.ApprovedAt = null;
                await context.SaveChangesAsync();

                await _invoicesService.Remove(record);
            }
            else if (!record.ApprovedAt.HasValue)
            {
                record.ApprovedAt = DateTime.Now;
                await context.SaveChangesAsync();

                await _invoicesService.Add(record);
            }

        }

        public override async Task<List<RecordOFX>> List()
        {
            return await context.RecordOFX
                                .OrderBy(x => x.Date)
                                .ThenByDescending(x => x.Value)
                                .ToListAsync();
        }

        public async Task<List<RecordOFX>> List(RecordOFXFilter filter)
        {
            return await context.RecordOFX
                                .Where(x => (!filter.Month.HasValue || x.Date.Month == filter.Month.Value)
                                         && (!filter.Year.HasValue || x.Date.Year == filter.Year.Value)
                                         && (!filter.Bank.HasValue || x.Bank == filter.Bank.Value))
                                .OrderBy(x => x.Date)
                                .ThenByDescending(x => x.Value)
                                .ToListAsync();
        }

        public async Task UpdateGroup(RecordOFX model)
        {
            var record = await context.RecordOFX.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (record == null)
                return;

            record.Group = model.Group;
            record.SubGroupId = null;

            await context.SaveChangesAsync();
        }

        public async Task UpdateSubGroup(RecordOFX model)
        {
            var record = await context.RecordOFX.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (record == null)
                return;

            record.SubGroupId = model.SubGroupId;

            await context.SaveChangesAsync();
        }

        public async Task UpdateUser(RecordOFX model)
        {
            var record = await context.RecordOFX.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (record == null)
                return;

            record.UserId = model.UserId;

            await context.SaveChangesAsync();
        }

        public async Task<decimal> GetIncomeTotalValue() => await context.RecordOFX.Where(x => x.ApprovedAt.HasValue
                                                                                               && (x.Group == RecordOFXGroup.Agency
                                                                                                   || x.Group == RecordOFXGroup.Intermediation))
                                                                                   .SumAsync(x => x.Value);
        public async Task<decimal> GetGroupTotalValue(RecordOFXGroup group) => await context.RecordOFX.Where(x => x.ApprovedAt.HasValue
                                                                                                                    && x.Group == group)
                                                                                                      .SumAsync(x => x.Value);
    }
}
