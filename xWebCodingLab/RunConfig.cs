using System;

namespace xWebCodingLab
{
    public class RunConfig
    {
        // xWeb
        public string xWebUrl { get; set; }
        public string username{ get; set; }
        public string password { get; set; }

        // All transactions
        public string cst_key { get; set; }

        // Subscription
        public string prd_key { get; set; }
    }
}
