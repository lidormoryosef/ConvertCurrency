using System.Text.Json;
using WebApplication2.ViewModels;

namespace WebApplication2.Utils;

public class Utils
{
    private static string _base = "?base=EUR"; //// If You want to change base You only need to change here and variable baseC in Index.
    public static List<string> GetDays()
    {
        DateTime today = DateTime.Now;
        int offset = -(int)today.DayOfWeek + (int)DayOfWeek.Monday; // If I want to pick another day instead monday, I only need to change this row. 
        if (offset > 0)
            offset = -6;
        DateTime lastDayToCheck = today.AddDays(offset);
        List<string> last7DaysToCheck = new List<string>();
        for (int i = 0; i < 7; i++) // If I want more than last 7 , I only need to change the number in for.
            last7DaysToCheck.Add(lastDayToCheck.AddDays(-7 * i).ToString("yyyy-MM-dd"));
        return last7DaysToCheck; 
    }

    public async static Task<List<WeekValue>?> GetValues(List<string> last7DaysToCheck,List<string> currencies)
    {
        string url = "https://api.frankfurter.app/";
        using HttpClient client = new HttpClient();
        List<WeekValue> daysValues = new List<WeekValue>();
            foreach (var lastDayToCheck in last7DaysToCheck)
            {
                var values = new List<double>();
                FrankfurterResponse? frankfurterResponse = await GetWeekValue(url + lastDayToCheck + _base, client);
                if (frankfurterResponse == null)
                    continue;
                foreach(var currency in currencies)
                    values.Add(frankfurterResponse.Rates[currency]);
                daysValues.Add(new WeekValue()
                {
                    Date = lastDayToCheck,
                    Values = values
                });
            }
            return daysValues;
    }

    private async static Task<FrankfurterResponse?> GetWeekValue(string url, HttpClient client) {
        try {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            FrankfurterResponse? frankfurterResponse =
                JsonSerializer.Deserialize<FrankfurterResponse>(responseBody);
            return frankfurterResponse;
        }   
        catch (HttpRequestException e) {
            return null;
        }

    }

    public static List<string> CheckUnique(List<string> currencies)
    {
        Dictionary<string, bool> currenciesDictionary = new Dictionary<string, bool>();
        foreach (var currency in currencies)
        {
            if (currenciesDictionary.TryGetValue(currency, out bool value))
                continue;
            currenciesDictionary.Add(currency, true);
        }
        return currenciesDictionary.Keys.ToList();
    }
}