using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Amazon.DynamoDBv2;

namespace Web.App.Controllers
{
    public class BaseApiController : ApiController
    {
        protected Web.App.Service.Service Service;
        protected AmazonDynamoDBClient client;

        public BaseApiController()
        {
            client = HttpContext.Current.Application.Get("DbClient") as AmazonDynamoDBClient;
            this.Service = new Service.Service(client);
        }
    }
}
