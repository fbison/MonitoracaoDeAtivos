using MonitoramentoAcao.Models;
using System.Reflection.Metadata;

namespace MonitoramentoAcao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            if(args.Length < 3) throw new ArgumentException("Valores de configuração faltantes");
            if (!(double.TryParse(args[1], out double referenciaVenda) && double.TryParse(args[2], out double referenciaCompra)))
                throw new ArgumentException("Argumento inválido para valores de referência.");
            
            builder.Services.Configure<ParametrosWorker>(settings =>
            {
                settings.ChaveAtivo = args[0];
                settings.ReferenciaVenda = referenciaVenda;
                settings.ReferenciaCompra = referenciaCompra;
            });

            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}