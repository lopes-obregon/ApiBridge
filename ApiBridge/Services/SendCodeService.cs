using ApiBridge.Models.Dto;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace ApiBridge.Services
{
    public class SendCodeService
    {
        private readonly IConfiguration _configuration;
        public SendCodeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        internal async Task<bool> SendCodToEmail(SyncUserDto user)
        {
            var smtpHost = _configuration["SmtpSettings:Host"];
            var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
            var username = _configuration["SmtpSettings:Username"];
            var password = _configuration["SmtpSettings:Password"];
            var senderEmail = _configuration["SmtpSettings:SenderEmail"];
            var senderName = _configuration["SmtpSettings:SenderName"];
            using var client = new SmtpClient(smtpHost, smtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(username, password);
            client.EnableSsl = true; // Obrigatório para o Gmail na porta 587
            client.DeliveryMethod = SmtpDeliveryMethod.Network; // força envio na rede.
            
            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail, senderName),
                Subject = "Confirme seu e-mail - Gestor MEI",
                Body = $@"
                <h2>Olá {user.Name} Seja Bem-vindo!</h2>
                <p>Para ativar sua conta, utilize o código/link abaixo:</p>
                <p><b>{user.code}</b></p>
                <br>
                <p>Se você não solicitou este cadastro, desconsidere este e-mail.</p>",
                IsBodyHtml = true
            };
            if (!String.IsNullOrEmpty(user.Email))
            {
                mailMessage.To.Add(user.Email);
            }
            try
            {
                await client.SendMailAsync(mailMessage);
                return true; // email enviado com sucesso

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false; // falha ao enviar o email
            }

        }
    }
}
