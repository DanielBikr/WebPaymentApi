using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPaymentApi.Service
{
    public class DeclineInstructions
    {
        public DeclineInstructions(string code, string description, string nextSteps)
        {
            Code = code;
            Description = description;
            NextSteps = nextSteps;
        }

        public string Code { get; set; }
        public string Description { get; set; }
        public string NextSteps { get; set; }
    }
}