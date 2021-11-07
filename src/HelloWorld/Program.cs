using System;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using Samples.Helper;

namespace HelloWorld
{
    class Program
    {
        static void Main()
        {
            ApiContext context = AppSettingHelper.GetApiContext();

            context.ApiLogManager = new ApiLogManager();
            context.ApiLogManager.ApiLoggerList.Add(new FileLogger("log.txt", true, true, true));
            context.ApiLogManager.EnableLogging = true;
            context.Site = SiteCodeType.US;

            GeteBayOfficialTimeCall apiCall = new GeteBayOfficialTimeCall(context);

            DateTime officialTime = apiCall.GeteBayOfficialTime();

            Console.WriteLine(String.Format("Official eBay Time: {0}", officialTime));
            Console.WriteLine();
            Console.WriteLine("Press any key to close this application...");
            Console.ReadKey();
        }
    }
}
