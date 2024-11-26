using WebApplication2.ViewModels;

namespace WebApplication2.Service;

public class HomeService
{
    public async Task<ConvertResponse?> GetValues(List<string> currencies)
    {
        List<string> uniqueCurrencies = Utils.Utils.CheckUnique(currencies);
        List<string> dateDays = Utils.Utils.GetDays();
        var valuesDays =await Utils.Utils.GetValues(dateDays, uniqueCurrencies);
        if (valuesDays == null)
            return null;
        return new ConvertResponse()
        {
            Currencies = uniqueCurrencies,
            WeekValues = valuesDays
        };
    }
}