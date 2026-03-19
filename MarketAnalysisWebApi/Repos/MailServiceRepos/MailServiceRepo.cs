using MailKit.Net.Smtp;
using MarketAnalysisWebApi.Providers;
using MarketAnalysisWebApi.Providers.Configuration;
using MimeKit;

namespace MarketAnalysisWebApi.Repos.MailServiceRepos
{
    public class MailServiceRepo : IMailServiceRepo
    {
        private readonly IInnerConfigurationProvider _config;

        private const int RetryTimes = 5;
        private const int WaitTime = 500;

        private const string TriecoName = "Компания СМК-ГИДРИКС";
        private const string TriecoAddr = "market@gsurso.ru";

        private const string ConfirmCodeSubject = "Ваш код подтверждения";
        private const string PasswordSubject = "Ваш пароль";

        public MailServiceRepo(IInnerConfigurationProvider config)
        {
            _config = config;
        }

        public async Task Send(EmailReceiver receiver, string subject, string message)
        {
            using var emailMessage = new MimeMessage();
            var connData = _config.GetEmailServerConnData();
            var authData = _config.GetEmailAuthData();

            emailMessage.From.Add(new MailboxAddress(TriecoName, TriecoAddr));
            emailMessage.To.Add(new MailboxAddress(receiver.Name, receiver.Address));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var smtpClient = new SmtpClient();

            var tries = 0;

            while (tries < RetryTimes)
            {
                tries++;
                try
                {
                    await smtpClient.ConnectAsync(connData.Host, connData.Port, connData.UseSsl);
                    await smtpClient.AuthenticateAsync(authData.Username, authData.Password);
                    await smtpClient.SendAsync(emailMessage);
                    await smtpClient.DisconnectAsync(true);
                    break;
                }
                catch
                {
                    await Task.Delay(WaitTime);
                }
            }
        }

        public async Task SendConfirmCode(EmailReceiver receiver, string confirmCode)
        {
            var message = $@"Здравствуйте, {receiver.Name}!
Вы зарегистрировались в автоматизированной системе Trieco.
Ваш логин: {receiver.Login}
Код подтверждения электронной почты: {confirmCode}";

            await Send(receiver, ConfirmCodeSubject, message);
        }

        public async Task SendPassword(EmailReceiver receiver, string password)
        {
            var message = $@"Здравствуйте, {receiver.Name}!
Вы зарегистрировались в автоматизированной системе Trieco.
Ваш логин: {receiver.Login}
Ваш пароль: {password}";

            await Send(receiver, PasswordSubject, message);
        }

        public async Task RecoveryPassword(EmailReceiver receiver, string password)
        {
            var message = $@"Здравствуйте! Вы запросили сброс пароля в автоматизированной система Terieco.
Ваш новый пароль: {password}";

            await Send(receiver, PasswordSubject, message);
        }
    }
}
