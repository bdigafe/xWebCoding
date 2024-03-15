using System;
using System.Configuration;

namespace xWebCodingLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("This is sample code to test specicific xWeb functions.");
            RunSubscriptionProforma();
        }

        private static void RunSubscriptionProforma()
        {
            // Setup config
            var config = new RunConfig
            {
                // CHANGE: Update the app.config file to use a different site
                xWebUrl = ConfigurationManager.AppSettings["xWebUrl"],
                username = ConfigurationManager.AppSettings["xWebUserName"],
                password = ConfigurationManager.AppSettings["xWebUserPassword"],

                // CHANGE: Update this to choose a different customer key 
                cst_key = "59CA9CAE-EE8D-4F14-8BFD-0000362D416C",

                // CHANGE: Update to invoice for a specific subscription product. If empty, a random subscription product is choosen
                prd_key = string.Empty
            };

            // Setup xWeb
            Logger.Info("Connect to xWeb", true);
            var xWeb = new xWebWrapper(config.xWebUrl, config.username, config.password);

            // Run option
            var runner = new SubscriptionProformaPurchase();
            runner.RunOption(xWeb, config);

            // Program completed
            Console.WriteLine("Press any key to terminate!");
            Console.ReadKey();
        }
    }
}
