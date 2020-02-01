using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace PokerGameAPI.Results
{
    public class ValdationResult
    {
        public bool Status { get; set; }
        public string Message { get; }
        public HttpError ModelState { get; }

        public ValdationResult(bool status, string message, ModelStateDictionary modelState)
        {
            Status = status;
            Message = message;
            ModelState = new HttpError(modelState, includeErrorDetail: true).ModelState;
        }
    }
}