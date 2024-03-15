using System;
using System.Reflection;
using xWebCodingLab.xWebNetForumXML;
using log4net;

namespace xWebCodingLab
{
    public class SubscriptionProformaPurchase : IRunOption
    {
        // Instantiate logger
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void RunOption(xWebWrapper xWeb, RunConfig config)
        {
            // Get an API Token
            xWeb.GetApiToken();

            // Get customer primary address
            var cst_key = new Guid(config.cst_key);
            Logger.Info("Getting individual information...");
            var individualInfo = xWeb.Client.WEBIndividualGet(cst_key);

            if (individualInfo == null)
            {
                Logger.Error($"Unable to get individual record: {config.cst_key}");
                return;
            }
            var cxa_key = individualInfo.Customer.cst_cxa_key;

            if (string.IsNullOrEmpty(cxa_key))
            {
                Logger.Error($"Customer has no primary address. Address is required for shipping or billing purposes. Customer: {config.cst_key}");
                return;
            }

            // Get xWeb and instantiate a new cart
            Logger.Info("Setting up an empty shopping cart", true);
            CentralizedOrderEntryType _cart = xWeb.SetupNewCart(cst_key);

            // Get a subscription product
            var sub_prd_key = config.prd_key;
            if (string.IsNullOrEmpty(sub_prd_key))
            {
                Logger.Info("Getting list of subscription products...");
                var subcriptionsList = xWeb.Client.WEBCentralizedShoppingCartGetSubscriptionList_Ignore_PC();

                if (subcriptionsList != null && subcriptionsList.ChildNodes.Count > 0)
                {
                    sub_prd_key = subcriptionsList.ChildNodes[0]["prc_prd_key"].InnerText;
                    Logger.Info(subcriptionsList.InnerText);
                }
            }
            Guid prd_key = Guid.Parse(sub_prd_key);

            // Get subscription line item
            Logger.Info("Add a subscription product to the shopping cart ...");
            InvoiceDetailType invoiceDetail = xWeb.Client.WEBCentralizedShoppingCartGetProductLineItem(prd_key, cst_key, Guid.Parse(cxa_key));

            //Add the line item to the cart
            _cart = xWeb.Client.WEBCentralizedShoppingCartAddLineItem(_cart, invoiceDetail);

            // Set Proforma and ensure Payment is not auto-applied by COE
            _cart.Invoice.inv_proforma = 1;
            _cart.Invoice.inv_proformaSpecified = true;
            _cart.Invoice.inv_orig_trans_type = "Proforma";
            _cart.Invoice.inv_use_payment_to_apply_field = 1;
            _cart.Invoice.inv_use_payment_to_apply_fieldSpecified = true;

            // Submit 
            var message = string.Empty;
            try
            {
                Logger.Info("Submitting the shopping cart...");
                _cart = xWeb.Client.WEBCentralizedShoppingCartInsert(_cart);
                Logger.Info($"Cart submission completed without errors. Invoice: {_cart.Invoice.inv_code}", true);
            }
            catch (Exception ex)
            { 
                message = ex.Message;
                Console.WriteLine(message);
                Logger.Error("Error submitting the COE", ex,true);
            }

            Logger.Info("Process completed.");
        }
    }
}