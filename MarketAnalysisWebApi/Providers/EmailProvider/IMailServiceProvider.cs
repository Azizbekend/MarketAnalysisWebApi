using MarketAnalysisWebApi.DTOs.UserDTOs;
using MarketAnalysisWebApi.Models.Configuration;
using MarketAnalysisWebApi.Providers;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Providers.EmailProvider
{
    public interface IMailServiceProvider
    {
        public Task Send(EmailReceiver receiver, string subject, string message);
        public Task SendConfirmCode(EmailReceiver receiver, string confirmCode);
        public Task SendPassword(EmailReceiver receiver, string password);
        public Task RecoveryPassword(EmailReceiver receiver, string password);

    }
}
