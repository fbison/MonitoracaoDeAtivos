using MonitoramentoAcao.Models;

namespace MonitoramentoAcao.Interfaces;

public interface IMonitoramentoService : IDisposable
{
    Task MonitoraAtivo();

}
