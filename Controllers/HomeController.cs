using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPaymentApi.Service;

namespace WebPaymentApi.Controllers
{
    [RoutePrefix("api")]
    public class HomeController : Controller
    {
        private MusjroomStripeService service;

        public MusjroomStripeService Service
        {
            get
            {
                if (service == null)
                    service = new MusjroomStripeService();
                return service;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HandlePayment()
        {//pass token

            var result = Service.ExecuteCharge(Request.Form);

            if (result.Exception != null)
                return View("Something went wrong. ");

            if (result.DeclineInstructions != null)
            {
                var declineInstructions = result.DeclineInstructions;
                string msg = declineInstructions.Description;
                msg += $"\n";
                return View(result.DeclineInstructions.Description);
            }

            //succes
            return View();
        }

        /// <summary>
        /// IDEAL BETALINGEN, bij het betalen met ideal komt de functie hier binnen
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SourcePayment()
        {
            string owner = Request.Form.GetValues("owner")[0];
            int amount = int.Parse(Request.Form.GetValues("amount")[0]);
            var stripeSource = Service.CreateSource(amount, owner);

            if (stripeSource == null)
            {
                //see stripe source for any explination on why it went wrong
                //provide user with info
                ViewBag.PaymentProces = "Payment failed.";
                return View();
            }

            ViewBag.PaymentProces = "Redirecting...";
            return Redirect(stripeSource.Redirect.Url);
        }

        [HttpPost]
        public ActionResult CreditCardPayment()
        {//
            var result = Service.ExecuteCharge(Request.Form);

            return View();
        }


        private void LogException(Exception ex)
        {
            string msg = "Error:";
            if (ex.InnerException != null)
                msg += $"{Environment.NewLine}{ex.InnerException}";

            //LOG to DB or textfile
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}
    }
}