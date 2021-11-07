using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using Samples.Helper;

namespace eBayNotifications
{
    class Program
    {
        private static ApiContext _apiContext;

        static void Main()
        { 
            // Setup the eBay context
            _apiContext = AppSettingHelper.GetApiContext();
            _apiContext.ApiLogManager = new ApiLogManager();
            _apiContext.ApiLogManager.ApiLoggerList.Add(new FileLogger(AppSettingHelper.LOG_FILE_NAME, true, true, true));
            _apiContext.ApiLogManager.EnableLogging = true;
            _apiContext.Site = SiteCodeType.US;

            // Inform eBay about the events we want to be notified about...
            SetNotificationPreferencesCall apiCall = new SetNotificationPreferencesCall(_apiContext);
            apiCall.Version = _apiContext.Version;
            apiCall.Site = _apiContext.Site;
            apiCall.EnableCompression = true;
            
            // How do we want to receive the notifications?
            ApplicationDeliveryPreferencesType applicationDeliveryPreferences = new ApplicationDeliveryPreferencesType();
            applicationDeliveryPreferences.ApplicationEnable = EnableCodeType.Enable;
            applicationDeliveryPreferences.ApplicationEnableSpecified = true;
            applicationDeliveryPreferences.ApplicationURL = "mailto://geersch@gmail.com";
            applicationDeliveryPreferences.AlertEmail = "mailto://geersch@gmail.com";
            applicationDeliveryPreferences.AlertEnable = EnableCodeType.Enable;
            applicationDeliveryPreferences.AlertEnableSpecified = true;

            // Which events to we want to be notified about?
            NotificationEnableType notification =
                new NotificationEnableType
                    {
                        EventEnable = EnableCodeType.Enable,
                        EventEnableSpecified = true,
                        EventType = NotificationEventTypeCodeType.FixedPriceTransaction,
                        EventTypeSpecified = true
                    };

            apiCall.ApplicationDeliveryPreferences = applicationDeliveryPreferences;
            apiCall.UserDeliveryPreferenceList = new NotificationEnableTypeCollection(new[] { notification });
            apiCall.Execute();
        }
    }
}
