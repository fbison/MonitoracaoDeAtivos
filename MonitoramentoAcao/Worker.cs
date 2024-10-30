using Microsoft.Extensions.Options;
using MonitoramentoAcao.Interfaces;
using MonitoramentoAcao.Models;
using System.Globalization;

namespace MonitoramentoAcao
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMonitoramentoService _monitoramentoService;

        private readonly int INTERVALOms = 60000; //1 minuto
        public Worker(ILogger<Worker> logger,
            IConfiguration configuration, IMonitoramentoService monitoramentoService)
        {
            _logger = logger;
            _monitoramentoService = monitoramentoService;
            string? intervalo = configuration["Worker:IntervaloMs"];
            if(intervalo != null && 
                !(int.TryParse(intervalo, out INTERVALOms))){
                throw new ArgumentException("Intervalo em arquivo de configurações está incorreto");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _monitoramentoService.MonitoraAtivo();
                _logger.LogInformation($"Processo reiniciará em {INTERVALOms/1000} s");
                await Task.Delay(INTERVALOms, stoppingToken);
            }
        }
    }
}
