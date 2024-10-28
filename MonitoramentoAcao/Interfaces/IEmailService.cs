using MonitoramentoAcao.Models;

namespace MonitoramentoAcao.Interfaces;

public interface IEmailService : IDisposable
{
    Task AconselharVenda(string ativo, double valorAcao, double valorReferenciaVenda);
    Task AconselharCompra(string ativo, double valorAcao, double valorReferenciaVenda);

}
