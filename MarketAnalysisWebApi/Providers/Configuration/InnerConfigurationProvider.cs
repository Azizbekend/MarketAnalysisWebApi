using MarketAnalysisWebApi.Extensions;
using MarketAnalysisWebApi.Models.Configuration;

namespace MarketAnalysisWebApi.Providers.Configuration
{
    public class InnerConfigurationProvider : IInnerConfigurationProvider
    {
        private const string EmailServerConnKey = "HostEmailData:ConnectData";
        private const string EmailAuthKey = "HostEmailData:AuthData";
        //private const string SmsApiKey = "SmsApiData";
        //private const string RedisNotifyChanKey = "Redis:NotificationsChan";
        //private const string RedisPositionChanKey = "Redis:PositionChan";
        //private const string PayKeeperKey = "PayKeeperApi:BasicKey";

        private readonly IConfiguration _configuration;

        public InnerConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public EmailAuthData GetEmailAuthData()
        {
            var result = _configuration.GetSectionAs<EmailAuthData>(EmailAuthKey);
            if (result is null)
            {
                throw new Exception(
                    $"Email auth data section is not presented. Expected key: {EmailAuthKey}");
            }

            return result;
        }

        public EmailServerData GetEmailServerConnData()
        {
            var result = _configuration.GetSectionAs<EmailServerData>(EmailServerConnKey);
            if (result is null)
            {
                throw new Exception(
                    $"Email server connection section is not presented. Expected key: {EmailServerConnKey}");
            }

            return result;
        }

        public string GetNotificationChannel()
        {
            throw new NotImplementedException();
        }

        public string GetPayKeeperBasicAuth()
        {
            throw new NotImplementedException();
        }

        public string GetPositionChannel()
        {
            throw new NotImplementedException();
        }
    }
}
