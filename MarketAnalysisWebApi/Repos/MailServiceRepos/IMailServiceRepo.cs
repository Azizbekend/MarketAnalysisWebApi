using MarketAnalysisWebApi.Providers;
using MarketAnalysisWebApi.Models.Configuration;

namespace MarketAnalysisWebApi.Repos.MailServiceRepos
{
    public interface IMailServiceRepo
    {
        public Task Send(EmailReceiver receiver, string subject, string message);
        public Task SendConfirmCode(EmailReceiver receiver, string confirmCode);
        public Task SendPassword(EmailReceiver receiver, string password);
        public Task RecoveryPassword(EmailReceiver receiver, string password);
    }
}
