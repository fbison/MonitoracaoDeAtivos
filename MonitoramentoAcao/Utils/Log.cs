using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace MonitoramentoAcao.Utils;


public class Log<T> : IDisposable
{
    private readonly ILogger<T> _logger;
    private readonly Stopwatch _timer;
    private readonly string _processo;

    public Log(ILogger<T> logger, string processo)
    {
        _logger = logger;
        _processo = processo;
        _timer = Stopwatch.StartNew();
        _logger.LogInformation("Inicio Processo: {Processo}", _processo);
    }

    public void LogWarningExecucao(string mensagem)
    {
        if (_timer.IsRunning)
        {
            _timer.Stop();
            _logger.LogWarning("WARNING: Processo: {Processo} - {Tempo} - {Mensagem}",
                               _processo, _timer.Elapsed.ToString("\\.hh\\:mm\\:ss\\.fff"), mensagem);
            _timer.Start();
        }
    }

    public void LogErro(string mensagemDeErro)
    {
        if (_timer.IsRunning)
        {
            _timer.Stop();
            _logger.LogError("ERRO: Processo: {Processo} - {Tempo} - {MensagemDeErro}",
                             _processo, _timer.Elapsed.ToString("\\.hh\\:mm\\:ss\\.fff"), mensagemDeErro);
            _timer.Start();
        }
    }
    public Exception LogErroComExcessao(string mensagemDeErro)
    {
        if (!_timer.IsRunning) new Exception("Erro: LogErroComExcessao chamado incorretamente");
        _timer.Stop();
        string mensagem = string.Format("ERRO: Processo: {Processo} - {Tempo} - {MensagemDeErro}",
                             _processo, _timer.Elapsed.ToString("\\.hh\\:mm\\:ss\\.fff"), mensagemDeErro);
        _logger.LogError(mensagem);
        return new Exception(mensagem);
    }
    public void LogInternoExecucao(string aviso)
    {
        if (_timer.IsRunning)
        {
            _timer.Stop();
            _logger.LogInformation("Processo: {Processo} - {Tempo} - {aviso}", _timer.Elapsed.ToString("\\.hh\\:mm\\:ss\\.fff"), _processo, aviso);
            _timer.Stop();
        }
    }

    public void Dispose()
    {
        if (_timer.IsRunning)
        {
            _timer.Stop();
            _logger.LogInformation("Fim Processo: {Processo} - {Tempo}",
                                   _processo, _timer.Elapsed.ToString("\\.hh\\:mm\\:ss\\.fff"));
        }
    }
}
