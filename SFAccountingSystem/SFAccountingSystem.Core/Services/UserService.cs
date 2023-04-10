using SFAccountingSystem.Core.Models;

namespace SFAccountingSystem.Core.Services
{
    public class UserService : BaseService<User>
    {
        public UserService(DataContext context) : base(context)
        {
        }
    }
}
