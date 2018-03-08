using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPaymentApi.Service
{
    public class CustomResponse
    {
        public Exception Exception { get; set; }
        public DeclineInstructions DeclineInstructions { get; set; }
        public string Message { get; set; }

        public bool Succes { get; set; }
    }
}