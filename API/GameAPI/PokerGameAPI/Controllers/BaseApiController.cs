using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokerGameAPI.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PokerGameAPI.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        public IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("errorlist", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return Content(HttpStatusCode.BadRequest,
                    new ValdationResult(false, "The request is invalid.", ModelState));
            }

            return null;
        }
        public IHttpActionResult GetResult(bool status, object data, string message)
        {
            if (ModelState.IsValid)
            {
                string datastr = JsonConvert.SerializeObject(data);
                ModelState.AddModelError("data", datastr);
                return Content(HttpStatusCode.OK,
                    new ValdationResult(status, message, ModelState));
            }

            return null;
        }
        public IHttpActionResult GetResult(bool status, List<object> datas, string message)
        {
            if (ModelState.IsValid)
            {
                foreach (object obj in datas)
                {
                    string datastr = JsonConvert.SerializeObject(obj);
                    ModelState.AddModelError("data", datastr);
                }
                return Content(HttpStatusCode.OK,
                    new ValdationResult(status, message, ModelState));
            }

            return null;
        }
        public IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }
    }
}