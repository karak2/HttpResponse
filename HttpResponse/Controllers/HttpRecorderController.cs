using System.Collections.Generic;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;

namespace HttpResponse.Controllers
{
    public class HttpRecorderController : ApiController
    {
        // GET: api/HttpRecorder
        public string Get()
        {
            var ip = GetClientIPAddress();
            if (string.IsNullOrEmpty(ip))
            {
                ip = GetClientIp2();
            }
            return ip;
        }

        public static string GetClientIPAddress()
        {
            var context = System.Web.HttpContext.Current;
            var sIPAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(sIPAddress))
            {
                return context.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                var ipArray = sIPAddress.Split(new char[] { ',' });
                return ipArray[0];
            }
        }


        private string GetClientIp2(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return "No client IP found";
            }
        }
    }
}
