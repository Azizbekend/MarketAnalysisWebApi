using System.ComponentModel;

namespace MarketAnalysisWebApi.Helpers.EmailTemplate
{
    public class EmailContainer
    {


        public string Style { get; set; }
        public const string PassworRecoveryBody = $@"<div class=""greeting"">
    Здравствуйте{{ ' , ' + receiver.Name ?? ''}}!
</div>

<div class=""message-text"">
    Вы запросили сброс пароля в системе <strong>КликПроект</strong>.
    Ваш пароль был успешно изменен. Ниже приведены данные для входа:
</div>

<div class=""password-container"">
    <div class=""password-wrapper"">
        <span class=""password-label"">Ваш новый пароль</span>
        <div class=""password-value"">
            {{password}}
            <button class=""copy-button"" data-clipboardtext=""{{password}}"">
                <svg width=""24"" height=""24"" viewBox=""0 0 24 24"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                    <path d=""M9 15H5C3.89543 15 3 14.1046 3 13V5C3 3.89543 3.89543 3 5 3H13C14.1046 3 15 3.89543 15 5V9M11 21H19C20.1046 21 21 20.1046 21 19V11C21 9.89543 20.1046 9 19 9H11C9.89543 9 9 9.89543 9 11V19C9 20.1046 9.89543 21 11 21Z"" stroke=""white"" stroke-width=""2"" stroke-linecap=""round"" stroke-linejoin=""round""></path>
                </svg>
            </button>
        </div>
    </div>
</div>

<div class=""warning"">
    <span class=""warning-icon"">⚠️</span>
    <div>
        <strong>Важно:</strong> Для безопасности рекомендуется сменить этот пароль
        после первого входа в систему.
    </div>
</div>

<ul class=""info-list"">
    <li>Пароль чувствителен к регистру</li>
    <li>Рекомендуем сохранить пароль в надежном месте</li>
    <li>При возникновении проблем свяжитесь со службой поддержки</li>
</ul>

<div style=""text-align: center;"">
    <a href=""http://localhost:5173/"" class=""button"">Войти в систему</a>
</div>";
        string RegisterBody = $@"<div class=""greeting"">
    Здравствуйте{{ ' , ' + receiver.Name ?? ''}}!
</div>

<div class=""message-text"">
    Вы успешно прошли регистрацию в систему <strong>КликПроект</strong>.
    Ниже приведены данные для входа:
</div>

<div class=""password-container"">
    <div class=""password-wrapper"">

        <span class=""password-label _second"">Ваш новый пароль</span>
        <div class=""password-value"">
            <span>{{password}}</span>
            <button class=""copy-button"" data-clipboardtext=""{{password}}"">
                <svg width=""24"" height=""24"" viewBox=""0 0 24 24"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                    <path d=""M9 15H5C3.89543 15 3 14.1046 3 13V5C3 3.89543 3.89543 3 5 3H13C14.1046 3 15 3.89543 15 5V9M11 21H19C20.1046 21 21 20.1046 21 19V11C21 9.89543 20.1046 9 19 9H11C9.89543 9 9 9.89543 9 11V19C9 20.1046 9.89543 21 11 21Z"" stroke=""white"" stroke-width=""2"" stroke-linecap=""round"" stroke-linejoin=""round""></path>
                </svg>
            </button>
        </div>
    </div>
</div>



<div class=""warning"">
    <span class=""warning-icon"">⚠️</span>
    <strong>Важно:</strong> Для безопасности рекомендуется сменить этот пароль
    после первого входа в систему.
</div>

<div style=""text-align: center;"">
    <a href=""http://localhost:5173/"" class=""button"">Войти в систему</a>
</div>";

        string container = $@"<?php function emailHtml($styles, $body)
{{ ?>


    <!DOCTYPE html>
    <html lang='ru'>

    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Восстановление пароля - КликПроект</title>
        <style>
            $
                body {{
                margin: 0;
                padding: 0;
                font-family: 'Segoe UI', 'Helvetica Neue', Arial, sans-serif;
                line-height: 1.6;
                box-sizing: border-box;
            }}

            .container {{
                margin: 0 auto;
                /* max-width: 600px; */
                /* padding: 20px; */
            }}

            .email-wrapper {{
                background-color: #ffffff;
                overflow: hidden;
                box-shadow: 0 8px 24px rgba(0, 0, 0, 0.08);
            }}

            .header {{
                /* background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); */
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
                /* background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); */
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

            @media only screen and (max-width: 480px) {{
                .container {{
                    padding: 10px;
                }}

                .content {{
                    padding: 25px 20px;
                }}

                .password-value {{
                    font-size: 24px;
                    /* padding: 12px 16px; */
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
            const passBtn = document.querySelector("".copy-button"");
            const passValue = passBtn.dataset.clipboardtext;
            if (passBtn) {{
                passBtn.addEventListener(""click"", () => {{
                    navigator.clipboard.writeText(passValue).then(() => {{
                        passBtn.style.backgroundColor = ""#28a745"";
                        setTimeout(() => {{
                            passBtn.style.backgroundColor = ""#2b7fff""
                        }}, 1000)
                    }}).catch(err => {{
                        console.error('Ошибка копирования:', err);
                    }});
                }});
            }}
        </script>
    </body>

    </html>

<?php }} ?>";
    }
}
