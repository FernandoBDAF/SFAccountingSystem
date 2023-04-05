using System.ComponentModel.DataAnnotations;

namespace SFAccountingSystem.Core.Enums
{
    public enum UserType
    {
        [Display(Name = "Regular")]
        Regular = 0,

        [Display(Name = "Shareholders")]
        Shareholders = 1
    }
}
