using log4net;
using System;
using System.Reflection;
using xWebCodingLab.xWebNetForumXML;

namespace xWebCodingLab
{
    public class xWebWrapper
    {
        public netForumXML Client = null;
        private string username;
        private string password;

        // Instantiate logger
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // Token control
        private AuthorizationToken AuthorizationTokenValue = null;
        DateTime _tokenExpireTime = DateTime.Now.AddMinutes(-1);

        public xWebWrapper(string url, string username, string password)
        {
            this.username = username;
            this.password = password;

            // Connect
            Logger.Info($"Trying to connect to xWeb and download the WSDL. URL:\n {url}", true);
            Client = new netForumXML();
        }


        /// <summary>
        /// Get an API Token from cache or create a new one and store the new token in a cache.
        /// </summary>
        public void GetApiToken()
        {
            if (_tokenExpireTime > DateTime.Now && AuthorizationTokenValue != null)
            {
                log.Info("Token request satisified from the cache");
                return;
            }

            // Get/store Token
            int CacheMinutes = 15;
            Logger.Info("Requesting a new API Token...", true);
            Client.Authenticate(username, password);
            AuthorizationTokenValue = Client.AuthorizationTokenValue;
            _tokenExpireTime = DateTime.Now.AddMinutes(15);
            Logger.Info($"Token obtained and it is cached for {CacheMinutes} minutes", true);
        }

        /// <summary>
        /// Validate user's email and password for login purposes
        /// </summary>
        /// <param name="szUserName"></param>
        /// <param name="szPassword"></param>
        /// <returns></returns>
        public WebUserType LoginCustomer(string szUserName, string szPassword)
        {
           // Authenticate the app, if needed
            GetApiToken();

            // Invoke
            return Client.WEBWebUserValidateLogin(szUserName, szPassword);
        }

        /// <summary>
        /// Validate a customer login token for SSO purposes.
        /// </summary>
        /// <param name="szToken"></param>
        /// <returns></returns>
        public WebUserType ValidateCustomerToken(string szToken)
        {
            // Authenticate the app, if needed
            GetApiToken();

            // Invoke
            return Client.WEBWebUserValidateToken(szToken);
        }

        /// <summary>
        /// Setup a new COE for a customer.
        /// </summary>
        /// <param name="szCustomerKey"></param>
        /// <returns></returns>
        public CentralizedOrderEntryType SetupNewCart(Guid szCustomerKey)
        {
           // Authenticate the app, if needed
            GetApiToken();

            // Setup a new Cart
            return Client.WEBCentralizedShoppingCartGetNew(szCustomerKey);
        }

    }
}