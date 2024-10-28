using Newtonsoft.Json;
namespace MonitoramentoAcao.Models;

public class ConfiguracoesEmail
{
    [JsonProperty("EmailDestino")]
    public string EmailDestino { get; set; }

    [JsonProperty("EmailOrigem")]
    public string EmailOrigem { get; set; }

    [JsonProperty("ServidorSmtp")]
    public string ServidorSmtp { get; set; }

    [JsonProperty("Porta")]
    public int Porta { get; set; }

    [JsonProperty("UsarSSL")]
    public bool UsarSSL { get; set; }

    [JsonProperty("NomeUsuario")]
    public string NomeUsuario { get; set; }

    [JsonProperty("Senha")]
    public string Senha { get; set; }
}

