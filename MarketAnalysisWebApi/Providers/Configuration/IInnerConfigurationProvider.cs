using MarketAnalysisWebApi.Models.Configuration;

namespace MarketAnalysisWebApi.Providers.Configuration
{
    public interface IInnerConfigurationProvider
    {
        EmailServerData GetEmailServerConnData();
        EmailAuthData GetEmailAuthData();
        //SmsApiData GetSmsApiData();
        string GetNotificationChannel();
        string GetPositionChannel();
        string GetPayKeeperBasicAuth();
    }
}
