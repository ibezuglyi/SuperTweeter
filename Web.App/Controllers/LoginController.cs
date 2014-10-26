using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Web.App.Model.DDB;

namespace Web.App.Controllers
{
    public class LoginController : BaseApiController
    {
        public void Post(User user)
        {
            var dbUser = this.Service.GetUser(user.UserName);
            if (dbUser.Password == user.Password)
            {
                DynamoSession.User = user;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        public void Delete()
        {
            DynamoSession.User = null;
        }
    }
}
