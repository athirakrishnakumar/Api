using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ApiCRUDE
{
    public class AutherizationRequiredAttribute:ActionFilterAttribute
    {
        private const string Token = "Token";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var provider = new ServiceClass();

            if(actionContext.Request.Headers.Contains(Token))
            {
                var tokenvalue = actionContext.Request.Headers.GetValues(Token).First();

                if(provider!=null && !provider.ValidateToken(tokenvalue))
                {
                    var responsemessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "invalid" };
                    actionContext.Response = responsemessage;
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            base.OnActionExecuting(actionContext);
        }
    }
}