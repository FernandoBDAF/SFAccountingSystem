using Microsoft.AspNetCore.Http;
using SFAccountingSystem.Core.Enums;

namespace SFAccountingSystem.Core.ViewModels
{
    public class ObjectFileViewModel
    {
        public IFormFile? Ofx { get; set; }

        public RecordOFXBank Bank { get; set; }
    }
}
