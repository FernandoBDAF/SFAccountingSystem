using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Models;

namespace SFAccountingSystem.Services
{
    public class RecordOFXService
    {
        private readonly OFXService _ofxService;
        private readonly DataContext _dataContext;

        public RecordOFXService(OFXService ofxService, DataContext dataContext)
        {
            _ofxService = ofxService;
            _dataContext = dataContext;
        }

        public async Task Add(IFormFile file)
        {
            foreach (var transaction in _ofxService.Process(file))
            {
                await _dataContext.RecordOFX.AddAsync(new RecordOFX(transaction));
            }

            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<RecordOFX>> List()
        {
            return await _dataContext.RecordOFX.OrderByDescending(x => x.Date).ThenByDescending(x => x.Value).ToListAsync();
        }
    }
}
