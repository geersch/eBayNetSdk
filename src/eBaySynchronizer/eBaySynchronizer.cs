using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using Samples.Helper;
using eBayApi = eBay.Service;

namespace eBaySynchronizer
{
    public abstract class eBaySynchronizer
    {
        #region Fields

        private static readonly ApiContext _apiContext;

        #endregion

        #region Constructor(s)

        static eBaySynchronizer()
        {
            _apiContext = AppSettingHelper.GetApiContext();
            _apiContext.ApiLogManager = new ApiLogManager();
            _apiContext.ApiLogManager.ApiLoggerList.Add(new eBayApi.Util.FileLogger("log.txt", true, true, true));
            _apiContext.ApiLogManager.EnableLogging = true;
            _apiContext.Site = SiteCodeType.US;
        }

        #endregion

        #region Properties

        protected static ApiContext ApiContext { get { return _apiContext; } }

        #endregion

        #region Methods

        public abstract void Synchronize();

        #endregion
    }
}
