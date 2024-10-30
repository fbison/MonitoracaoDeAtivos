using Microsoft.Extensions.Options;
using MonitoramentoAcao.Interfaces;
using MonitoramentoAcao.Models;

namespace MonitoramentoAcao
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMonitoramentoService _monitoramentoService;
        private readonly int INTERVALOms = 60000; //1 minuto
        public Worker(ILogger<Worker> logger, IMonitoramentoService monitoramentoService)
        {
            _logger = logger;
            _monitoramentoService = monitoramentoService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _monitoramentoService.MonitoraAtivo();
                _logger.LogInformation($"Processo reiniciará em {INTERVALOms}");
                await Task.Delay(INTERVALOms, stoppingToken);
            }
        }
    }
}
