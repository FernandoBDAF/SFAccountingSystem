using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SFAccountingSystem.Core.Models;
using System.Text.Json.Serialization;

namespace SFAccountingSystem.Core.Services
{
    public class IntermediationsService : BaseService<Intermediation>
    {
        public IntermediationsService(DataContext context) : base(context)
        {
        }

        public override async Task<List<Intermediation>> List()
        {
            return await context.Intermediation.Include(x => x.RecordsOFXes).ToListAsync();
        }

        public async Task<List<RecordOFX>> GetNotBounded()
        {
            return await context.RecordOFX.Include(x => x.User)
                                          .Where(x => x.Group == Enums.RecordOFXGroup.Intermediation
                                                      && x.ApprovedAt.HasValue
                                                      && !x.IntermediationId.HasValue)
                                          .ToListAsync();
        }

        public override async Task Delete(int id)
        {
            var intermediation = await context.Intermediation.Include(x => x.RecordsOFXes)
                                                             .FirstOrDefaultAsync(x => x.Id == id);

            if (intermediation != null)
            {
                foreach (var item in intermediation.RecordsOFXes)
                {
                    item.IntermediationId = null;
                    context.RecordOFX.Update(item);
                }

                context.Intermediation.Remove(intermediation);
                await context.SaveChangesAsync();
            }
        }

        public override async Task<Intermediation> Get(int id)
        {
            return await context.Intermediation.Include(x => x.RecordsOFXes)
                                               .ThenInclude(x => x.User)
                                               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<Intermediation> Add(Intermediation obj)
        {
            if (obj.RecordOfxIds.Any())
            {
                foreach (var item in obj.RecordOfxIds)
                {
                    var recordOfx = await context.RecordOFX.FirstOrDefaultAsync(x => x.Id == item);

                    if (recordOfx != null)
                    {
                        obj.RecordsOFXes.Add(recordOfx);
                    }

                }
            }

            return await base.Add(obj);
        }

        public async Task CreateInvoice(Intermediation model)
        {
            var existing = await context.Intermediation
                                        .FirstOrDefaultAsync(x => x.Id == model.Id);

            existing.Value = model.Value;
            existing.InvoiceUserIds = JsonConvert.SerializeObject(model.UserIds);

            await context.SaveChangesAsync();
        }
    }
}
