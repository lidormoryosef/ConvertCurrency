

namespace WebApplication2.ViewModels;

public class ConvertResponse
{
    public List<string> Currencies { get; set; }
    public List<WeekValue> WeekValues { get; set; }
}

public class WeekValue
{
    public string Date { get; set; }
    public List<double> Values { get; set; }
}
