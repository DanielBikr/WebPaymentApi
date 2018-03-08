using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPaymentApi.Service
{
    public interface IStripeService
    {
        /// <summary>
        /// Charges apply to credit card payments
        /// </summary>
        /// <param name="token"></param>
        /// <param name="chargeOptions"></param>
        /// <returns></returns>
        CustomResponse ExecuteCharge(NameValueCollection formPostValues);
        StripeCharge CreateAndCaptureCharge(string token, StripeChargeCreateOptions chargeOptions = null);
        StripeCharge AuthorizeCharge(string token, StripeChargeCreateOptions chargeOptions);
        StripeCharge CaptureAuthorizedCharge();
        StripeCustomer CreateCustomer(StripeCustomerCreateOptions customerOptions);
        StripeRefund CreateRefund(string chargeId);

        /// <summary>
        /// Sources apply to IDeal Payments
        /// </summary>
        /// <returns></returns>
        StripeSource CreateSource(int amount, string owner);
    }
}
