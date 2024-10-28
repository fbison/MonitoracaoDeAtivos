using MonitoramentoAcao.Models;

namespace MonitoramentoAcao.Interfaces;

public interface IApiService: IDisposable
{
    Task<Result> BuscaDadosDeAtivo(string chaveAtivo);
}
