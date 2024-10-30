using M3AI.Imp.Referencia.Base;
using MonitoramentoAcao.Interfaces;
using MonitoramentoAcao.Models;
using System.Globalization;
using System.Reflection.Metadata;

namespace MonitoramentoAcao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            if(args.Length < 3) throw new ArgumentException("Valores de configuração faltantes");
            if (!(double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double referenciaVenda) &&
                    double.TryParse(args[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double referenciaCompra)))
                throw new ArgumentException("Argumento inválido para valores de referência.");
            
            builder.Services.Configure<ParametrosWorker>(settings =>
            {
                settings.ChaveAtivo = args[0];
                settings.ReferenciaVenda = referenciaVenda;
                settings.ReferenciaCompra = referenciaCompra;
            });

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IMonitoramentoService, MonitoramentoService>();

            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}