using JOIEnergy.Enums;

namespace JOIEnergy.Services.Account
{
    public interface IAccountService
    {
        Supplier GetPricePlanIdForSmartMeterId(string smartMeterId);
    }
}