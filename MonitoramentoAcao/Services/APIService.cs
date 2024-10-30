using MonitoramentoAcao.Interfaces;
using MonitoramentoAcao.Models;
using MonitoramentoAcao.Utils;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace M3AI.Imp.Referencia.Base
{
    public class ApiService : IApiService
    {
        private readonly ILogger<ApiService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public ApiService(ILogger<ApiService> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _client = _httpClientFactory.CreateClient();
        }

        private T FormataResposta<T>(string responseText) where T : new()
        {
            if (string.IsNullOrEmpty(responseText)) return new T();
            T? responseFormatada = JsonConvert.DeserializeObject<T>(responseText);
            if (responseFormatada != null) return responseFormatada;
            throw new JsonSerializationException();
        }

        private async Task<T> ConsultaGetEndpoint<T>(string endpoint) where T : new()
        {
            using (Log<ApiService> log = new Log<ApiService>(_logger, "Consulta endpoint " + endpoint))
                try
                {
                    string? urlBase = _configuration["API:UrlBase"];
                    string? token = _configuration["API:Token"];
                    if (string.IsNullOrWhiteSpace(urlBase) || string.IsNullOrEmpty(token))
                    {
                        throw log.LogErroComExcessao("Dados da API não configurados corretamente no projeto");
                    }
                    using (var request = new HttpRequestMessage(HttpMethod.Get, urlBase + endpoint))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        HttpResponseMessage response = await _client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        string responseText = await response.Content.ReadAsStringAsync();
                        
                        return FormataResposta<T>(responseText);
                    }
                }
                catch (JsonSerializationException jsonEx)
                {
                    throw log.LogErroComExcessao("Erro na deserialização do JSON recebido do endpoint");
                }
                catch (Exception ex)
                {
                    throw log.LogErroComExcessao(ex.Message);
                }
        }

        public async Task<Result> BuscaDadosDeAtivo(string chaveAtivo)
        {
            using (Log<ApiService> log = new Log<ApiService>(_logger, $"Busca de chave: {chaveAtivo} ")) {
                var response = await ConsultaGetEndpoint<ResponseData>(_configuration["API:Endpoint_BuscaAtivo"] +chaveAtivo);
                if (response.Results.Count > 1) throw log.LogErroComExcessao("Api obteve um comportamento inesperado enviando mais de um resultado para uma só ação");
                return response.Results.FirstOrDefault();
            }
                
        }
        public void Dispose()
        {

        }
    }
}
