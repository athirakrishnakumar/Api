using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ApiCRUDE
{
    public class MyCustomResult : IHttpActionResult
    {
        string _value;

        HttpRequestMessage _request;

        HttpStatusCode _code;

        public MyCustomResult(string value, HttpRequestMessage request,HttpStatusCode code)
        {
            _value = value;
            //_password = password;
            _request = request;
            _code = code;  
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(_value),
               
                RequestMessage =_request,
                StatusCode=_code

            };
            return Task.FromResult(response);
        }
    }
}