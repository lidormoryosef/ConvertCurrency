using System.Text.Json.Serialization;

namespace WebApplication2.ViewModels;
using System.Collections.Generic;
public class FrankfurterResponse
{
    [JsonPropertyName("rates")]
    public Dictionary<string, double> Rates { get; set; }
}