using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HttpResponse.Controllers
{
    public class HttpRecorderController : ApiController
    {
        // GET: api/HttpRecorder
        public IEnumerable<string> Get()
        {
            return new string[] { "This is HttpRecorderController!" };
        }
    }
}
