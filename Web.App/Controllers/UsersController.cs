using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Amazon.DynamoDBv2;
using Web.App.Model.DDB;

namespace Web.App.Controllers
{
    public class UsersController : BaseApiController
    {

        public List<string> Get()
        {
            return Service.GetAllUsers();
        }

        [HttpGet]
        public User Get(string id)
        {
            var user = this.Service.GetUser(id);
            return user;
        }
        [HttpPost]
        public void Post(User user)
        {
            this.Service.CreateUser(user.UserName, user.Password, user.FullName);
        }

    }
}