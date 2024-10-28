using Microsoft.Extensions.Options;
using MonitoramentoAcao.Interfaces;
using MonitoramentoAcao.Models;

namespace MonitoramentoAcao
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMonitoramentoService _monitoramentoService;

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
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
