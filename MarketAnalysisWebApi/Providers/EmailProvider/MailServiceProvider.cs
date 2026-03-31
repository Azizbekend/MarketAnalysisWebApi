using MailKit.Net.Smtp;
using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DTOs.UserDTOs;
using MarketAnalysisWebApi.Providers;
using MarketAnalysisWebApi.Providers.Configuration;
using MarketAnalysisWebApi.Repos.BaseRepo;
using MimeKit;

namespace MarketAnalysisWebApi.Providers.EmailProvider
{
    public class MailServiceProvider : IMailServiceProvider
    {
        private readonly IInnerConfigurationProvider _config;

        private const int RetryTimes = 5;
        private const int WaitTime = 500;

        private const string SenderName = "Платформа КликПроект";
        private const string SenderAddr = "market@gsurso.ru";

        private const string ConfirmCodeSubject = "Ваш код подтверждения";
        private const string PasswordRecoverySubject = "КликПроект. Сброс пароля.";
        private const string RegisterSubject = "КликПроект. Подтверждение регистрации в системе.";

        public MailServiceProvider(IInnerConfigurationProvider config)
        {
            _config = config;
        }

        public async Task Send(EmailReceiver receiver, string subject, string message)
        {
            using var emailMessage = new MimeMessage();
            var connData = _config.GetEmailServerConnData();
            var authData = _config.GetEmailAuthData();
            emailMessage.From.Add(new MailboxAddress(SenderName, SenderAddr));
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
Код подтверждения электронной почты: {confirmCode}";

            await Send(receiver, ConfirmCodeSubject, message);
        }

        public async Task SendPassword(EmailReceiver receiver, string password)
        {
            var message = $@"<!DOCTYPE html>
<html lang=""ru"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Восстановление пароля - КликПроект</title>
</head>

<body
    style=""margin:0; padding:0; background-color:#f0f0f0; font-family: 'Segoe UI', 'Helvetica Neue', Arial, sans-serif;"">

    <center style=""width:100%; background-color:#f0f0f0;"">
        <table align=""center"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0""
            style=""max-width:600px; width:100%; background-color:#f0f0f0;"" class=""container"">
            <tr>
                <td align=""center"" style=""padding:20px 0;"">

                    <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#ffffff""
                        style=""background-color:#ffffff; border-radius:8px; box-shadow:0 2px 8px rgba(0,0,0,0.1);"">

                        <tr>
                            <td bgcolor=""#2b7fff""
                                style=""background-color:#2b7fff; padding:40px 20px; text-align:center;"">
                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        <td align=""center""
                                            style=""font-size:32px; font-weight:bold; color:#ffffff; font-family:'Segoe UI','Helvetica Neue',Arial,sans-serif;"">
                                            КликПроект
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align=""center""
                                            style=""font-size:14px; color:rgba(255,255,255,0.9); padding-top:10px;"">
                                            Управляйте проектами с легкостью
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <!-- CONTENT -->
                        <tr>
                            <td class=""content-padding"" style=""padding:40px 30px;"">

                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        <td class=""greeting""
                                            style=""font-size:24px; color:#2c3e50; font-weight:600; padding-bottom:20px;"">
                                            Здравствуйте, {receiver.Name}!
                                        </td>
                                    </tr>
                                    <tr>
                                        <td
                                            style=""font-size:16px; color:#5a6c7e; line-height:1.6; padding-bottom:25px;"">
                                            Вы успешно прошли регистрацию в систему <strong>КликПроект</strong>.
                                            Ниже приведены данные для входа:
                                        </td>
                                    </tr>
                                </table>

                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0""
                                    style=""margin-bottom:28px;"">
                                    <tr>
                                        <td
                                            style=""font-size:12px; font-weight:600; text-transform:uppercase; letter-spacing:1px; color:#64748b; padding-bottom:12px;"">
                                            Ваш новый пароль
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor=""#f8f9fa""
                                            style=""background-color:#f8f9fa; border-radius:10px; border:1px solid #e2e8f0; padding:20px 15px;"">
                                            <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                <tr>
                                                    <td
                                                        style=""font-family:'SF Mono','Monaco',monospace; font-size:20px; font-weight:700; text-align:center; color:#1e293b; word-break:break-all;"">
                                                        {password}
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""margin:25px 0;"">
                                    <tr>
                                        <td bgcolor=""#fff3e0""
                                            style=""background-color:#fff3e0; border-left:4px solid #ff9800; padding:15px 20px; border-radius:8px;"">
                                            <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                <tr>
                                                    <td width=""30"" valign=""top"" style=""font-size:20px;"">⚠️</td>
                                                    <td style=""font-size:14px; color:#856404;"">
                                                        <strong>Важно:</strong> Для безопасности рекомендуется сменить
                                                        этот пароль после первого входа в систему.
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        <td align=""center"" style=""padding:20px 0 10px 0;"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""margin:0 auto;"">
                                                <tr>
                                                    <td align=""center"" bgcolor=""#2b7fff""
                                                        style=""background-color:#2b7fff; border-radius:25px;"">
                                                        <a href=""http://market.gsurso.ru/login""
                                                            style=""display:inline-block; background-color:#2b7fff; color:#ffffff; font-family:'Segoe UI','Helvetica Neue',Arial,sans-serif; font-size:16px; font-weight:600; text-decoration:none; padding:12px 30px; border-radius:25px;"">
                                                            Войти в систему
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>

                        <tr>
                            <td bgcolor=""#f8f9fa""
                                style=""background-color:#f8f9fa; padding:25px 30px; text-align:center; border-top:1px solid #e9ecef;"">
                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        <td style=""font-size:12px; color:#95a5a6; padding-bottom:10px;"">
                                            © 2024 КликПроект. Все права защищены.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size:12px; color:#95a5a6; padding-bottom:15px;"">
                                            Это письмо было отправлено автоматически, пожалуйста, не отвечайте на него.
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table>

                </td>
            </tr>
        </table>
    </center>

</body>

</html>";

            await Send(receiver, RegisterSubject, message);
        }

        public async Task RecoveryPassword(EmailReceiver receiver, string password)
        {
                  var message = $@"<!DOCTYPE html>
<html lang=""ru"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Восстановление пароля - КликПроект</title>
</head>

<body
    style=""margin:0; padding:0; background-color:#f0f0f0; font-family: 'Segoe UI', 'Helvetica Neue', Arial, sans-serif;"">

    <center style=""width:100%; background-color:#f0f0f0;"">
        <table align=""center"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0""
            style=""max-width:600px; width:100%; background-color:#f0f0f0;"" class=""container"">
            <tr>
                <td align=""center"" style=""padding:20px 0;"">

                    <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#ffffff""
                        style=""background-color:#ffffff; border-radius:8px; box-shadow:0 2px 8px rgba(0,0,0,0.1);"">

                        <tr>
                            <td bgcolor=""#2b7fff""
                                style=""background-color:#2b7fff; padding:40px 20px; text-align:center;"">
                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        <td align=""center""
                                            style=""font-size:32px; font-weight:bold; color:#ffffff; font-family:'Segoe UI','Helvetica Neue',Arial,sans-serif;"">
                                            КликПроект
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align=""center""
                                            style=""font-size:14px; color:rgba(255,255,255,0.9); padding-top:10px;"">
                                            Управляйте проектами с легкостью
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <!-- CONTENT -->
                        <tr>
                            <td class=""content-padding"" style=""padding:40px 30px;"">

                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        <td class=""greeting""
                                            style=""font-size:24px; color:#2c3e50; font-weight:600; padding-bottom:20px;"">
                                            Здравствуйте, {receiver.Name}!
                                        </td>
                                    </tr>
                                    <tr>
                                        <td
                                            style=""font-size:16px; color:#5a6c7e; line-height:1.6; padding-bottom:25px;"">
                                            Вы запросили сброс пароля в системе КликПроект <br/>
                                            Ваш пароль был успешно изменен. Ваш новый пароль:
                                        </td>
                                    </tr>
                                </table>

                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0""
                                    style=""margin-bottom:28px;"">
                                    <tr>
                                        <td
                                            style=""font-size:12px; font-weight:600; text-transform:uppercase; letter-spacing:1px; color:#64748b; padding-bottom:12px;"">
                                            Ваш новый пароль
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor=""#f8f9fa""
                                            style=""background-color:#f8f9fa; border-radius:10px; border:1px solid #e2e8f0; padding:20px 15px;"">
                                            <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                <tr>
                                                    <td
                                                        style=""font-family:'SF Mono','Monaco',monospace; font-size:20px; font-weight:700; text-align:center; color:#1e293b; word-break:break-all;"">
                                                        {password}
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""margin:25px 0;"">
                                    <tr>
                                        <td bgcolor=""#fff3e0""
                                            style=""background-color:#fff3e0; border-left:4px solid #ff9800; padding:15px 20px; border-radius:8px;"">
                                            <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                <tr>
                                                    <td width=""30"" valign=""top"" style=""font-size:20px;"">⚠️</td>
                                                    <td style=""font-size:14px; color:#856404;"">
                                                        <strong>Важно:</strong> Для безопасности рекомендуется сменить
                                                        этот пароль после первого входа в систему.
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        <td align=""center"" style=""padding:20px 0 10px 0;"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""margin:0 auto;"">
                                                <tr>
                                                    <td align=""center"" bgcolor=""#2b7fff""
                                                        style=""background-color:#2b7fff; border-radius:25px;"">
                                                        <a href=""http://market.gsurso.ru/login""
                                                            style=""display:inline-block; background-color:#2b7fff; color:#ffffff; font-family:'Segoe UI','Helvetica Neue',Arial,sans-serif; font-size:16px; font-weight:600; text-decoration:none; padding:12px 30px; border-radius:25px;"">
                                                            Войти в систему
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>

                        <tr>
                            <td bgcolor=""#f8f9fa""
                                style=""background-color:#f8f9fa; padding:25px 30px; text-align:center; border-top:1px solid #e9ecef;"">
                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                    <tr>
                                        <td style=""font-size:12px; color:#95a5a6; padding-bottom:10px;"">
                                            © 2024 КликПроект. Все права защищены.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size:12px; color:#95a5a6; padding-bottom:15px;"">
                                            Это письмо было отправлено автоматически, пожалуйста, не отвечайте на него.
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table>

                </td>
            </tr>
        </table>
    </center>

</body>

</html>";


            await Send(receiver, PasswordRecoverySubject, message);
        }

    }
}
