using Newtonsoft.Json;
namespace MonitoramentoAcao.Models;

public class ResponseData
{
    [JsonProperty("results")]
    public List<Result> Results { get; set; }

    [JsonProperty("requestedAt")]
    public DateTime RequestedAt { get; set; }

    [JsonProperty("took")]
    public string Took { get; set; }
}

