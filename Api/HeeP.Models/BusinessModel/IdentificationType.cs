using System.ComponentModel;

namespace HeeP.Models.BusinessModel
{
    public enum IdentificationType
    {
        [Description("National Identification Number")]
        NationalId = 1,

        [Description("Password")]
        Passport = 2,

        [Description("Tax Payer Identification Number")]
        TaxpayerIdentificationNumber = 3
    }
}