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
            var message = $@"<!DOCTYPE html>
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
            line-height: 1.6;
            box-sizing: border-box;
        }}

        .container {{
            margin: 0 auto;
        }}

        .email-wrapper {{
            background-color: #ffffff;
            overflow: hidden;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.08);
        }}

        .header {{
            background-color: #2b7fff;
            padding: 40px 15px;
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



        .button {{
            display: inline-block;
            background-color: #2b7fff;
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

        .password-container {{
            margin-bottom: 28px;
        }}

        .password-label {{
            display: block;
            font-size: 12px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 1px;
            color: #64748b;
            margin-bottom: 12px;
        }}

        .password-value {{
            font-family: 'SF Mono', 'Monaco', monospace;
            font-size: 12px;
            font-weight: 500;
            color: #1e293b;
            background: #ffffff;
            border-radius: 10px;
            border: 1px solid #e2e8f0;
            display: inline-block;
            letter-spacing: 0.3px;
            word-break: break-all;
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 20px;
            padding: 10px 15px;
        }}

        .copy-button {{
            background: #2b7fff;
            color: white;
            border: none;
            border-radius: 8px;
            padding: 8px 8px;
            font-size: 12px;
            font-weight: 500;
            cursor: pointer;
            transition: all 0.2s ease;
        }}

        .copy-button:hover {{
            background: #1a66e6;
            transform: scale(1.02);
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

        @media only screen and (max-width: 480px) {{
            .container {{
                padding: 10px;
            }}

            .content {{
                padding: 25px 20px;
            }}

            .password-value {{
                font-size: 24px;
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
                    Здравствуйте, ${receiver.Name}!
                </div>

                <div class='message-text'>
                    Вы успешно прошли регистрацию в систему <strong>КликПроект</strong>.
                    Ниже приведены данные для входа:
                </div>

                <div class='password-container'>
                    <div class='password-wrapper'>
                        <span class='password-label'>Ваш новый пароль</span>
                        <div class='password-value'>
                            <span>${password}</span>
                            <button class='copy-button' data-clipboardtext='${password}'>
                                <svg width='24' height='24' viewBox='0 0 24 24' fill='none'
                                    xmlns='http://www.w3.org/2000/svg'>
                                    <path
                                        d='M9 15H5C3.89543 15 3 14.1046 3 13V5C3 3.89543 3.89543 3 5 3H13C14.1046 3 15 3.89543 15 5V9M11 21H19C20.1046 21 21 20.1046 21 19V11C21 9.89543 20.1046 9 19 9H11C9.89543 9 9 9.89543 9 11V19C9 20.1046 9.89543 21 11 21Z'
                                        stroke='white' stroke-width='2' stroke-linecap='round'
                                        stroke-linejoin='round' />
                                </svg>
                            </button>
                        </div>
                    </div>
                </div>

                <div class='warning'>
                    <span class='warning-icon'>⚠️</span>
                    <div>
                        <strong>Важно:</strong> Для безопасности рекомендуется сменить этот пароль
                        после первого входа в систему.
                    </div>
                </div>

                <div style='text-align: center;'>
                    <a href='http://market.gsurso.ru/login' class='button'>Войти в систему</a>
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

    <script>
        const passBtn = document.querySelector('.copy-button');
        const passValue = passBtn.dataset.clipboardtext;
        if (passBtn) {{
            passBtn.addEventListener('click', () => {{
                navigator.clipboard.writeText(passValue).then(() => {{
                    passBtn.style.backgroundColor = '#28a745';
                    setTimeout(() => {{
                        passBtn.style.backgroundColor = '#2b7fff'
                    }}, 1000)
                }}).catch(err => {{
                    console.error('Ошибка копирования:', err);
                }});
            }});
        }}
    </script>
</body>

</html>";

            await Send(receiver, PasswordSubject, message);
        }

        public async Task RecoveryPassword(EmailReceiver receiver, string password)
        {
            var message = $@"<!DOCTYPE html>
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
            line-height: 1.6;
            box-sizing: border-box;
        }}

        .container {{
            margin: 0 auto;
        }}

        .email-wrapper {{
            background-color: #ffffff;
            overflow: hidden;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.08);
        }}

        .header {{
            background-color: #2b7fff;
            padding: 40px 15px;
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



        .button {{
            display: inline-block;
            background-color: #2b7fff;
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

        .password-container {{
            margin-bottom: 28px;
        }}

        .password-label {{
            display: block;
            font-size: 12px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 1px;
            color: #64748b;
            margin-bottom: 12px;
        }}

        .password-value {{
            font-family: 'SF Mono', 'Monaco', monospace;
            font-size: 12px;
            font-weight: 500;
            color: #1e293b;
            background: #ffffff;
            border-radius: 10px;
            border: 1px solid #e2e8f0;
            display: inline-block;
            letter-spacing: 0.3px;
            word-break: break-all;
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 20px;
            padding: 10px 15px;
        }}

        .copy-button {{
            background: #2b7fff;
            color: white;
            border: none;
            border-radius: 8px;
            padding: 8px 8px;
            font-size: 12px;
            font-weight: 500;
            cursor: pointer;
            transition: all 0.2s ease;
        }}

        .copy-button:hover {{
            background: #1a66e6;
            transform: scale(1.02);
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

        @media only screen and (max-width: 480px) {{
            .container {{
                padding: 10px;
            }}

            .content {{
                padding: 25px 20px;
            }}

            .password-value {{
                font-size: 24px;
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
                    Здравствуйте, ${receiver.Name}!
                </div>

                <div class='message-text'>
                    Вы запросили сброс пароля в системе <strong>КликПроект</strong>.
                    Ваш пароль был успешно изменен. Ниже приведены данные для входа:
                </div>

                <div class='password-container'>
                    <div class='password-wrapper'>
                        <span class='password-label'>Ваш новый пароль</span>
                        <div class='password-value'>
                            <span>${password}</span>
                            <button class='copy-button' data-clipboardtext='${password}'>
                                <svg width='24' height='24' viewBox='0 0 24 24' fill='none'
                                    xmlns='http://www.w3.org/2000/svg'>
                                    <path
                                        d='M9 15H5C3.89543 15 3 14.1046 3 13V5C3 3.89543 3.89543 3 5 3H13C14.1046 3 15 3.89543 15 5V9M11 21H19C20.1046 21 21 20.1046 21 19V11C21 9.89543 20.1046 9 19 9H11C9.89543 9 9 9.89543 9 11V19C9 20.1046 9.89543 21 11 21Z'
                                        stroke='white' stroke-width='2' stroke-linecap='round'
                                        stroke-linejoin='round' />
                                </svg>
                            </button>
                        </div>
                    </div>
                </div>

                <div class='warning'>
                    <span class='warning-icon'>⚠️</span>
                    <div>
                        <strong>Важно:</strong> Для безопасности рекомендуется сменить этот пароль
                        после первого входа в систему.
                    </div>
                </div>

                <div style='text-align: center;'>
                    <a href='http://market.gsurso.ru/login' class='button'>Войти в систему</a>
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

    <script>
        const passBtn = document.querySelector('.copy-button');
        const passValue = passBtn.dataset.clipboardtext;
        if (passBtn) {{
            passBtn.addEventListener('click', () => {{
                navigator.clipboard.writeText(passValue).then(() => {{
                    passBtn.style.backgroundColor = '#28a745';
                    setTimeout(() => {{
                        passBtn.style.backgroundColor = '#2b7fff'
                    }}, 1000)
                }}).catch(err => {{
                    console.error('Ошибка копирования:', err);
                }});
            }});
        }}
    </script>
</body>

</html>"; 

            await Send(receiver, PasswordSubject, message);
        }

    }
}
