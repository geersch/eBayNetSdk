using System;
using System.Threading;

namespace eBaySynchronizer
{
    class Program
    {
        static void Main()
        {
            eBayData eBayData = new eBayData();

            // Synchronize data with eBay and repeat this every hour.
            TimerCallback timerDelegate = eBayData.Synchronize;
            new Timer(timerDelegate, null, new TimeSpan(0, 0, 1), new TimeSpan(1, 0, 0));

            Console.WriteLine("Press any key to terminate this application.");
            Console.WriteLine();
            Console.ReadKey();
        }
    }

    internal class eBayData
    {
        // This method is called by the timer delegate.
        public void Synchronize(object stateInfo)
        {
            Console.WriteLine("{0} Synchronizing eBay categories", DateTime.Now);

            eBayCategorySynchronizer synchronizer = new eBayCategorySynchronizer();
            synchronizer.Synchronize();

            Console.WriteLine("{0} Synchronization complete", DateTime.Now);
            Console.WriteLine();
        }
    }
}
