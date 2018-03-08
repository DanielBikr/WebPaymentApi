using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WebPaymentApi.Service.Base;

namespace WebPaymentApi.Service
{
    public class MusjroomStripeService : StripeConfig, IStripeService
    {
        public MusjroomStripeService()
        { }

        public CustomResponse ExecuteCharge(NameValueCollection formPostValues)
        {
            //TODO aanpassen zodat ook voor ideal charges kunnen worden uitgevoerd
            //Implementatie hieronder is voor CreditCard via checkout ui.
            

            var token = formPostValues.GetValues(Constants.StripeToken);
            var tokenType = formPostValues.GetValues(Constants.StripeTokenType);
            var stripeEmail = formPostValues.GetValues(Constants.StripeEmail);
            var amountToBePaid = formPostValues.GetValues(Constants.Amount);
            var location = formPostValues.GetValues(Constants.Location);

            //save card in customer object
            SaveToken();
            //when time is right createA Charge With Capture = True
            var charge = CreateAndCaptureCharge(GetToken().Id.ToString(), null);
            if(IsChargeValid(charge.Outcome))
            {
                return new CustomResponse
                {
                    Succes = true
                };
            }
            else
            {
                var instructions = DeclineHandling.List[charge.Outcome.Reason];

                return new CustomResponse
                {
                    Succes = false,
                    Message = instructions.Description + "\n" + instructions.NextSteps,
                };
            }
        }

        /// <summary>
        /// Create the charge and charge the account right away
        /// </summary>
        /// <param name="token"></param>
        /// <param name="chargeOptions"></param>
        /// <returns></returns>
        public StripeCharge CreateAndCaptureCharge(string token, StripeChargeCreateOptions chargeOptions = null)
        {
            // Charge the user's card:
            var charges = new StripeChargeService();
            var charge = charges.Create(new StripeChargeCreateOptions
            {
                //StripeSourceType.
                Amount = 1000,
                Currency = "eur",
                Capture = true,
                Description = "Example charge",
                SourceTokenOrExistingSourceId = token
            });

            return charge;
        }

        /// <summary>
        /// Create for authorization to be captured later
        /// </summary>
        /// <param name="token"></param>
        /// <param name="chargeOptions"></param>
        /// <returns></returns>
        public StripeCharge AuthorizeCharge(string token, StripeChargeCreateOptions chargeOptions)
        {
            // Charge the user's card:
            var chargeService = new StripeChargeService();
            var charge = chargeService.Create(chargeOptions);

            //CODE hieronder moet geplaatst worden in de functie die deze functie AuthorizeCharge gaat aanroepen.
            //var charge = charges.Create(new StripeChargeCreateOptions
            //{
            //    Amount = 1000,
            //    Currency = "eur",
            //    Capture = false,
            //    Description = "Example charge",
            //    SourceTokenOrExistingSourceId = token
            //});

            return charge;
        }

        public StripeCustomer CreateCustomer(StripeCustomerCreateOptions customerOptions)
        {
            //CODE hieronder moet geplaatst worden in de functie die deze functie AuthorizeCharge gaat aanroepen.
            //var customerOptions = new StripeCustomerCreateOptions()
            //{
            //    Description = "Customer for sophia.davis@example.com",
            //    SourceToken = "tok_amex"
            //};
            new Stripe.StripeAccount();
            var customerService = new StripeCustomerService();
            return customerService.Create(customerOptions);
        }

        private bool IsChargeValid(StripeOutcome chargeOutcome)
        {   //if not valid, display reason
            return chargeOutcome == null;
        }


        private StripeToken GetToken()
        {//ophalen van bewaarde token uit DB
            throw new NotImplementedException();
        }

        private void SaveToken()
        {//bewaren om te wachten op bevestiging merchant, daarna de token ophalen om een charge met capture uit te voeren

        }

        public StripeCharge CaptureAuthorizedCharge()
        {
            throw new NotImplementedException();
        }

        public StripeRefund CreateRefund(string chargeId)
        {
            throw new NotImplementedException();
        }

        public StripeSource CreateSource(int amount, string owner)
        {
            var options = new StripeSourceCreateOptions
            {
                Type = "ideal",
                Amount = amount,
                Currency = "eur",
                Owner = new StripeSourceOwner { Name = owner },
                RedirectReturnUrl = "http://Home/Index",
            };

            var source = new StripeSourceService();
            return source.Create(options);
        }
    }
}