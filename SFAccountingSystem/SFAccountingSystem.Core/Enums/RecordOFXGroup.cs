using System.ComponentModel.DataAnnotations;

namespace SFAccountingSystem.Core.Enums
{
    public enum RecordOFXGroup
    {
        [Display(Name = "Agency")]
        Agency = 0,

        [Display(Name = "Intermediation")]
        Intermediation = 1,

        [Display(Name = "Expenses")]
        Expenses = 2,

        [Display(Name = "Contributions and Withdrawals")]
        ContributionsWithdrawals = 3,

        [Display(Name = "Reversals")]
        Reversals = 4,

        [Display(Name = "Investments")]
        Investments = 5
    }
}
