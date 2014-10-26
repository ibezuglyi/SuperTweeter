using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.App.Controllers
{
    public class FollowerController : BaseApiController
    {
        public void Post(FollowerDto follower)
        {
            Service.AddFollowedUser(DynamoSession.User.UserName, follower.UserName);
        }
    }

}
