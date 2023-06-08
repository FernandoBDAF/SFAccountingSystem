using SFAccountingSystem.Core.Enums;
using SFAccountingSystem.Core.ViewModels;

namespace SFAccountingSystem.Core.Services
{
    public class BalanceSheetService
    {
        private readonly RecordOFXService _recordOFXService;

        public BalanceSheetService(RecordOFXService recordOFXService)
        {
            _recordOFXService = recordOFXService;
        }

        public async Task<List<BalanceSheetViewModel>> GetIncomes() => new List<BalanceSheetViewModel>()
        {
            new BalanceSheetViewModel()
            {
                Name = "Income",
                Value = await _recordOFXService.GetIncomeTotalValue(),
                Children = new List<BalanceSheetViewModel>()
                {
                    new BalanceSheetViewModel()
                    {
                        Name = RecordOFXGroup.Agency.ToName<RecordOFXGroup>(),
                        Value = await _recordOFXService.GetGroupTotalValue(RecordOFXGroup.Agency),
                    },
                    new BalanceSheetViewModel()
                    {
                        Name = RecordOFXGroup.Intermediation.ToName<RecordOFXGroup>(),
                        Value = await _recordOFXService.GetGroupTotalValue(RecordOFXGroup.Intermediation),
                    }
                }
            }
        };

        public async Task<List<BalanceSheetViewModel>> GetExpenses()
        {
            await Task.Yield();
            return new List<BalanceSheetViewModel>();
        }
    }
}
