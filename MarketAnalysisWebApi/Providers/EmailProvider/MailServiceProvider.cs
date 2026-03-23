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
        private const string PasswordSubject = "КликПроект. Сброс пароля.";

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
            var message = $@"Здравствуйте, {receiver.Name}!
Вы зарегистрировались в автоматизированной системе КликПроект.
Ваш пароль: {password}";

            await Send(receiver, PasswordSubject, message);
        }

        public async Task RecoveryPassword(EmailReceiver receiver, string password)
        {
            var message = $@"
    <!DOCTYPE html>
    <html lang='ru'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Восстановление пароля - КликПроект</title>
        <style>
            body {{
                margin: 0;
                padding: 0;
                font-family: 'Segoe UI', 'Helvetica Neue', Arial, sans-serif;
                background-color: #f4f7fc;
                line-height: 1.6;
            }}
            .container {{
                max-width: 600px;
                margin: 0 auto;
                padding: 20px;
            }}
            .email-wrapper {{
                background-color: #ffffff;
                border-radius: 16px;
                overflow: hidden;
                box-shadow: 0 8px 24px rgba(0, 0, 0, 0.08);
            }}
            .header {{
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                padding: 40px 30px;
                text-align: center;
            }}
            .logo {{
                font-size: 32px;
                font-weight: bold;
                color: #ffffff;
                margin-bottom: 10px;
            }}
            .logo-sub {{
                font-size: 14px;
                color: rgba(255, 255, 255, 0.9);
            }}
            .content {{
                padding: 40px 30px;
            }}
            .greeting {{
                font-size: 24px;
                color: #2c3e50;
                margin-bottom: 20px;
                font-weight: 600;
            }}
            .message-text {{
                color: #5a6c7e;
                font-size: 16px;
                margin-bottom: 25px;
            }}
            .password-container {{
                background: linear-gradient(135deg, #f5f7fa 0%, #e9ecef 100%);
                border-radius: 12px;
                padding: 25px;
                margin: 30px 0;
                text-align: center;
                border: 1px solid #e0e6ed;
            }}
            .password-label {{
                font-size: 14px;
                color: #7f8c8d;
                text-transform: uppercase;
                letter-spacing: 1px;
                margin-bottom: 12px;
                display: block;
            }}
            .password-value {{
                font-size: 32px;
                font-weight: bold;
                font-family: 'Courier New', 'Monaco', monospace;
                color: #2c3e50;
                background: #ffffff;
                padding: 15px 20px;
                border-radius: 8px;
                display: inline-block;
                letter-spacing: 2px;
                border: 1px solid #d0d7de;
                box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
            }}
            .warning {{
                background-color: #fff3e0;
                border-left: 4px solid #ff9800;
                padding: 15px 20px;
                margin: 25px 0;
                border-radius: 8px;
                font-size: 14px;
                color: #856404;
            }}
            .warning-icon {{
                font-size: 20px;
                margin-right: 10px;
                vertical-align: middle;
            }}
            .button {{
                display: inline-block;
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                color: #ffffff !important;
                text-decoration: none;
                padding: 12px 30px;
                border-radius: 25px;
                font-weight: 600;
                margin: 20px 0;
                transition: transform 0.2s;
            }}
            .button:hover {{
                transform: translateY(-2px);
            }}
            .info-list {{
                background-color: #f8f9fa;
                border-radius: 8px;
                padding: 20px;
                margin: 25px 0;
                list-style: none;
            }}
            .info-list li {{
                margin: 12px 0;
                color: #5a6c7e;
                font-size: 14px;
                display: flex;
                align-items: center;
            }}
            .info-list li:before {{
                content: '✓';
                color: #27ae60;
                font-weight: bold;
                margin-right: 10px;
            }}
            .footer {{
                background-color: #f8f9fa;
                padding: 25px 30px;
                text-align: center;
                border-top: 1px solid #e9ecef;
                font-size: 12px;
                color: #95a5a6;
            }}
            .footer a {{
                color: #667eea;
                text-decoration: none;
            }}
            .support {{
                margin-top: 15px;
                font-size: 12px;
            }}
            @media only screen and (max-width: 480px) {{
                .container {{
                    padding: 10px;
                }}
                .content {{
                    padding: 25px 20px;
                }}
                .password-value {{
                    font-size: 24px;
                    padding: 12px 16px;
                }}
                .greeting {{
                    font-size: 20px;
                }}
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='email-wrapper'>
                <div class='header'>
                    <div class='logo'>КликПроект</div>
                    <div class='logo-sub'>Управляйте проектами с легкостью</div>
                </div>
                
                <div class='content'>
                    <div class='greeting'>
                        Здравствуйте, {receiver.Name ?? "пользователь"}!
                    </div>
                    
                    <div class='message-text'>
                        Вы запросили сброс пароля в системе <strong>КликПроект</strong>. 
                        Ваш пароль был успешно изменен. Ниже приведены данные для входа:
                    </div>
                    
                    <div class='password-container'>
                        <span class='password-label'>Ваш новый пароль</span>
                        <div class='password-value'>{password}</div>
                    </div>
                    
                    <div class='warning'>
                        <span class='warning-icon'>⚠️</span>
                        <strong>Важно:</strong> Для безопасности рекомендуется сменить этот пароль 
                        после первого входа в систему.
                    </div>
                    
                    <ul class='info-list'>
                        <li>Пароль чувствителен к регистру</li>
                        <li>Рекомендуем сохранить пароль в надежном месте</li>
                        <li>При возникновении проблем свяжитесь со службой поддержки</li>
                    </ul>
                    
                    <div style='text-align: center;'>
                        <a href='https://clickproject.ru/login' class='button'>Войти в систему</a>
                    </div>
                </div>
                
                <div class='footer'>
                    <p>© 2024 КликПроект. Все права защищены.</p>
                    <p>Это письмо было отправлено автоматически, пожалуйста, не отвечайте на него.</p>
                    <div class='support'>
                        Нужна помощь? <a href='mailto:support@clickproject.ru'>support@clickproject.ru</a>
                    </div>
                </div>
            </div>
        </div>
    </body>
    </html>"; 

            await Send(receiver, PasswordSubject, message);
        }

    }
}
