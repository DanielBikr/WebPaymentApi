using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPaymentApi
{
    public static class Constants
    {
        //constants voor het uitlezen van de Request.Form velden bij het gebruik van checkout
        public const string StripeToken = "stripeToken";
        public const string StripeTokenType = "stripeTokenType";
        public const string StripeEmail = "stripeEmail";
        public const string Amount = "Amount";
        public const string Location = "Location";
        ////////

    }
}