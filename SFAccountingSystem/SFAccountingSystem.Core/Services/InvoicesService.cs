using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SFAccountingSystem.Core.Models;

namespace SFAccountingSystem.Core.Services
{
    public class InvoicesService : BaseService<Invoice>
    {
        public InvoicesService(DataContext context) : base(context)
        {
        }

        public override Task<List<Invoice>> List()
        {
            return context.Invoice.Include(x => x.User).ToListAsync();
        }

        public async Task Add(Intermediation intermediation)
        {
            if (intermediation != null
                && !string.IsNullOrEmpty(intermediation.InvoiceUserIds))
            {
                var userIds = JsonConvert.DeserializeObject<List<int>>(intermediation.InvoiceUserIds);

                if (userIds == null || userIds.Count() == 0)
                {
                    throw new Exception("Error!");
                }

                var recordOfxs = await context.RecordOFX.Where(x => x.IntermediationId == intermediation.Id)
                                                        .ToListAsync();

                recordOfxs = recordOfxs.Where(x => userIds.Any(o => o == x.UserId)).ToList();

                var totalValue = recordOfxs.Sum(x => x.Value > decimal.Zero ? x.Value : decimal.Negate(x.Value));

                foreach (var userId in userIds)
                {
                    var totalUser = recordOfxs.Where(x => x.UserId == userId)
                                              .Sum(x => x.Value > decimal.Zero ? x.Value : decimal.Negate(x.Value));

                    var invoice = new Invoice
                    {
                        UserId = userId,
                        CreatedAt = DateTime.Now,
                        IntermediationId = intermediation.Id,
                        Value = intermediation.Value * (totalUser / totalValue)
                    };

                    await context.Invoice.AddAsync(invoice);
                    await context.SaveChangesAsync();

                }

                return;
            }

            throw new Exception("Error!");
        }
    }
}
