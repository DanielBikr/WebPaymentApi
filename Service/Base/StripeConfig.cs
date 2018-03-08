using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stripe;

namespace WebPaymentApi.Service.Base
{
    public abstract class StripeConfig
    {
        /// <summary>
        /// TODO Verplaats velden hieronder naar WEBCONFIG tussen AppSettings en lees ze uit waar nodig met configurationmanager...
        /// </summary>
        /// 


        public string TestPublishAble { get; set; }
        public string TestSecretKey { get; set; }
        public string LivePublishableKey { get; set; }
        public string LiveSecretKey { get; set; }
        public StripeConfig()
        {//testapi key vervangen door live secret api key in dashboard bij het live gaan
            //StripeConfiguration.SetApiKey(testapikey);
            StripeConfiguration.SetApiKey(TestPublishAble);
        }
    }
}