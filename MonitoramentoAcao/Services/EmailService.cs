using MonitoramentoAcao.Interfaces;
using MonitoramentoAcao.Models;
using MonitoramentoAcao.Utils;
using System.Globalization;
using System.Net;
using System.Net.Mail;

namespace M3AI.Imp.Referencia.Base
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly ConfiguracoesEmail _configEmail;
        public EmailService(
            ILogger<EmailService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configEmail = configuration.GetSection("ConfiguracoesEmail").Get<ConfiguracoesEmail>();
            if (_configEmail == null) throw new Exception("Configurações de email faltantes no appSettings");
        }

        private async Task EnviarEmail(string assunto, string corpo)
        {
            using (Log<EmailService> log = new Log<EmailService>(_logger, "Envio de e-mail"))
            using (var cliente = new SmtpClient(_configEmail.ServidorSmtp, _configEmail.Porta))
            {
                cliente.EnableSsl = _configEmail.UsarSSL;
                cliente.Credentials = new NetworkCredential(_configEmail.NomeUsuario, _configEmail.Senha);

                var mensagemEmail = new MailMessage
                {
                    From = new MailAddress(_configEmail.EmailOrigem),
                    Subject = assunto,
                    Body = corpo,
                    IsBodyHtml = true
                };

                mensagemEmail.To.Add(_configEmail.EmailDestino);

                await cliente.SendMailAsync(mensagemEmail);
            }
        }

        public async Task AconselharVenda(string ativo, double valorAcao, double valorReferenciaVenda)
        {
            string assunto = $"Indicamos a venda do ativo: {ativo}";
            string mensagem = string.Format(CultureInfo.InvariantCulture,
                            "O valor do ativo {0} está igual a R$ {1:F2}, acima do limite de R$ {2:F2}. Por isso, aconselhamos a venda.",
                            ativo, valorAcao, valorReferenciaVenda);
            await EnviarEmail(assunto, mensagem);
        }

        public async Task AconselharCompra(string ativo, double valorAcao, double valorReferenciaVenda)
        {
            string assunto = $"Indicamos a compra do {ativo}";
            string mensagem = string.Format(CultureInfo.InvariantCulture,
                            "O valor do ativo {0} está igual a R$ {1:F2}, abaixo do limite de R$ {2:F2}. Por isso, aconselhamos a compra.",
                            ativo, valorAcao, valorReferenciaVenda);
            await EnviarEmail(assunto, mensagem);
        }


        public void Dispose()
        {

        }
    }
}
