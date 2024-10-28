using Newtonsoft.Json;
namespace MonitoramentoAcao.Models;

public class Result
{
    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("shortName")]
    public string ShortName { get; set; }

    [JsonProperty("longName")]
    public string LongName { get; set; }

    [JsonProperty("regularMarketChange")]
    public double RegularMarketChange { get; set; }

    [JsonProperty("regularMarketChangePercent")]
    public double RegularMarketChangePercent { get; set; }

    [JsonProperty("regularMarketTime")]
    public DateTime RegularMarketTime { get; set; }

    [JsonProperty("regularMarketPrice")]
    public double RegularMarketPrice { get; set; }

    [JsonProperty("regularMarketDayHigh")]
    public double RegularMarketDayHigh { get; set; }

    [JsonProperty("regularMarketDayRange")]
    public string RegularMarketDayRange { get; set; }

    [JsonProperty("regularMarketDayLow")]
    public double RegularMarketDayLow { get; set; }

    [JsonProperty("regularMarketVolume")]
    public long RegularMarketVolume { get; set; }

    [JsonProperty("regularMarketPreviousClose")]
    public double RegularMarketPreviousClose { get; set; }

    [JsonProperty("regularMarketOpen")]
    public double RegularMarketOpen { get; set; }

    [JsonProperty("fiftyTwoWeekRange")]
    public string FiftyTwoWeekRange { get; set; }

    [JsonProperty("fiftyTwoWeekLow")]
    public double FiftyTwoWeekLow { get; set; }

    [JsonProperty("fiftyTwoWeekHigh")]
    public double FiftyTwoWeekHigh { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("priceEarnings")]
    public double PriceEarnings { get; set; }

    [JsonProperty("earningsPerShare")]
    public double EarningsPerShare { get; set; }

    [JsonProperty("logourl")]
    public string LogoUrl { get; set; }
}


