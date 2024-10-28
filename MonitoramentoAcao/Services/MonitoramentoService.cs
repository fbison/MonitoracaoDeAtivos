using Microsoft.Extensions.Options;
using MonitoramentoAcao.Interfaces;
using MonitoramentoAcao.Models;
using MonitoramentoAcao.Utils;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace M3AI.Imp.Referencia.Base
{
    public class MonitoramentoService : IMonitoramentoService
    {
        private readonly ILogger<MonitoramentoService> _logger;
        private readonly ParametrosWorker _parametros;
        private readonly IApiService _apiService;
        private readonly IEmailService _emailService;
        protected MonitoramentoService(
            ILogger<MonitoramentoService> logger,
            IOptions<ParametrosWorker> options,
            IApiService apiService,
            IEmailService emailService)
        {
            _logger = logger;
            _parametros = options.Value;
            _apiService = apiService;
            _emailService = emailService;
        }

        public async Task MonitoraAtivo()
        {
            using (Log<MonitoramentoService> log = new Log<MonitoramentoService>(_logger, 
                $"Monitora Ativo {_parametros.ChaveAtivo}"))
            {
                Result resultado = await _apiService.BuscaDadosDeAtivo(_parametros.ChaveAtivo);
                if (resultado.RegularMarketPrice > _parametros.ReferenciaVenda)
                {
                    await _emailService.AconselharVenda(_parametros.ChaveAtivo, resultado.RegularMarketPrice, _parametros.ReferenciaVenda);
                }

                if (resultado.RegularMarketPrice < _parametros.ReferenciaCompra)
                {
                    await _emailService.AconselharCompra(_parametros.ChaveAtivo, resultado.RegularMarketPrice, _parametros.ReferenciaCompra);
                }
            }
        }
        public void Dispose()
        {

        }
    }
}
