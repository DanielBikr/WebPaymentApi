using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPaymentApi.Service
{
    public static class DeclineHandling
    {
        /// <summary>
        /// https://stripe.com/docs/declines/codes
        /// </summary>
        public static Dictionary<string, DeclineInstructions> List = new Dictionary<string, DeclineInstructions>
        {
            { "approve_with_id", new DeclineInstructions("approve_with_id", "The payment cannot be authorized.", "The payment should be attempted again.If it still cannot be processed, contact your card issuer.") },
            { "call_issuer", new DeclineInstructions("call_issuer","The card has been declined for an unknown reason.","Contact your card issuer for more information.") },
            { "card_not_supported", new DeclineInstructions("card_not_supported","The card does not support this type of purchase.","Contact your card issuer to make sure their card can be used to make this type of purchase.") },
            { "card_velocity_exceeded", new DeclineInstructions("card_velocity_exceeded","The customer has exceeded the balance or credit limit available on their card.","Contact your card issuer for more information.") },
            { "currency_not_supported", new DeclineInstructions("currency_not_supported","The card does not support the specified currency.","Check with your card issuer that the card can be used for the type of currency specified.") },
            { "do_not_honor", new DeclineInstructions("do_not_honor","The card has been declined for an unknown reason.","Contact your card issuer for more information.") },
            { "do_not_try_again", new DeclineInstructions("do_not_try_again","The card has been declined for an unknown reason.","The customer should contact their card issuer for more information.") },
            { "duplicate_transaction", new DeclineInstructions("duplicate_transaction","A transaction with identical amount and credit card information was submitted very recently.","Check to see if a recent payment already exists.") },
            { "expired_card", new DeclineInstructions("expired_card","The card has expired.","Try using another card.") },
            { "fraudulent", new DeclineInstructions("fraudulent","The payment has been declined as Stripe suspects it is fraudulent.","The card has been declined for an unknown reason. Contact your card issuer for more information.") },
            { "generic_decline", new DeclineInstructions("generic_decline","The card has been declined for an unknown reason.","Contact your card issuer for more information.") },
            { "incorrect_number", new DeclineInstructions("incorrect_number","The card number is incorrect.","Try again using the correct card number.") },
            { "incorrect_cvc", new DeclineInstructions("incorrect_cvc","The CVC number is incorrect.","Try again using the correct CVC.") },
            { "incorrect_pin", new DeclineInstructions("incorrect_pin","The PIN entered is incorrect. This decline code only applies to payments made with a card reader.","Try again using the correct PIN.") },
            { "incorrect_zip", new DeclineInstructions("incorrect_zip","The ZIP/postal code is incorrect.","Try again using a correct billing ZIP/postal code.") },
            { "insufficient_funds", new DeclineInstructions("insufficient_funds","The card has insufficient funds to complete the purchase.","Try an alternative payment method.") },
            { "invalid_account", new DeclineInstructions("invalid_account","The card, or account the card is connected to, is invalid.","Contact your card issuer to check that the card is working correctly.") },
            { "invalid_amount", new DeclineInstructions("invalid_amount","The payment amount is invalid, or exceeds the amount that is allowed.","If the amount appears to be correct, check with your card issuer to see if you can make purchases of that amount.") },
            { "invalid_cvc", new DeclineInstructions("invalid_cvc","The CVC number is incorrect.","Try again using the correct CVC.") },
            { "invalid_expiry_year", new DeclineInstructions("invalid_expiry_year","The expiration year invalid.","Try again using the correct expiration date.") },
            { "invalid_number", new DeclineInstructions("invalid_number","The card number is incorrect.","The customer should try again using the correct card number.") },
            { "invalid_pin", new DeclineInstructions("invalid_pin","The PIN entered is incorrect.This decline code only applies to payments made with a card reader.","Try again using the correct PIN.") },
            { "issuer_not_available", new DeclineInstructions("issuer_not_available","The card issuer could not be reached, so the payment could not be authorized.","The payment should be attempted again. If it still cannot be processed, the customer needs to contact their card issuer.") },
            { "lost_card", new DeclineInstructions("lost_card","The payment has been declined because the card is reported lost.","The specific reason for the decline should not be reported to the customer.Instead, it needs to be presented as a generic decline.") },
            { "new_account_information_available", new DeclineInstructions("new_account_information_available","The card, or account the card is connected to, is invalid.","The customer needs to contact their card issuer for more information.") },
            { "no_action_taken", new DeclineInstructions("no_action_taken","The card has been declined for an unknown reason.","The customer should contact their card issuer for more information.") },
            { "not_permitted", new DeclineInstructions("not_permitted","The payment is not permitted.","The customer needs to contact their card issuer for more information.") },
            { "pickup_card", new DeclineInstructions("pickup_card","The card cannot be used to make this payment (it is possible it has been reported lost or stolen).","The customer needs to contact their card issuer for more information.") },
            { "pin_try_exceeded", new DeclineInstructions("pin_try_exceeded","The allowable number of PIN tries has been exceeded.","The customer must use another card or method of payment.") },
            { "processing_error", new DeclineInstructions("processing_error","An error occurred while processing the card.","The payment should be attempted again. If it still cannot be processed, try again later.") },
            { "reenter_transaction", new DeclineInstructions("reenter_transaction","The payment could not be processed by the issuer for an unknown reason.","The payment should be attempted again. If it still cannot be processed, the customer needs to contact their card issuer.") },
            { "restricted_card", new DeclineInstructions("restricted_card","The card cannot be used to make this payment (it is possible it has been reported lost or stolen).","The customer needs to contact their card issuer for more information.") },
            { "revocation_of_all_authorizations", new DeclineInstructions("revocation_of_all_authorizations","The card has been declined for an unknown reason.","The customer should contact their card issuer for more information.") },
            { "revocation_of_authorization", new DeclineInstructions("revocation_of_authorization","The card has been declined for an unknown reason.","The customer should contact their card issuer for more information.") },
            { "security_violation", new DeclineInstructions("security_violation","The card has been declined for an unknown reason.","The customer needs to contact their card issuer for more information.") },
            { "service_not_allowed", new DeclineInstructions("service_not_allowed","The card has been declined for an unknown reason.","The customer should contact their card issuer for more information.") },
            { "stolen_card", new DeclineInstructions("stolen_card","The payment has been declined because the card is reported stolen.","The specific reason for the decline should not be reported to the customer.Instead, it needs to be presented as a generic decline.") },
            { "stop_payment_order", new DeclineInstructions("stop_payment_order","The card has been declined for an unknown reason.","The customer should contact their card issuer for more information.") },
            { "testmode_decline", new DeclineInstructions("testmode_decline","A Stripe test card number was used.","A genuine card must be used to make a payment.") },
            { "transaction_not_allowed", new DeclineInstructions("transaction_not_allowed","The card has been declined for an unknown reason.","The customer needs to contact their card issuer for more information.") },
            { "try_again_later", new DeclineInstructions("try_again_later","The card has been declined for an unknown reason.","Ask the customer to attempt the payment again. If subsequent payments are declined, the customer should contact their card issuer for more information.") },
            { "withdrawal_count_limit_exceeded", new DeclineInstructions("withdrawal_count_limit_exceeded","The customer has exceeded the balance or credit limit available on their card.","The customer should use an alternative payment method.") },
        };
    }
}