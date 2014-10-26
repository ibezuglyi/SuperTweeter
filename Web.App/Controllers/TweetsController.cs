using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Web.App.Model.DDB;

namespace Web.App.Controllers
{
    public class TweetsController : BaseApiController
    {
        public List<UserTweetDto> Get()
        {
            if (DynamoSession.User != null)
            {
                var result = Service.GetTweetsFromFollowedUsers(DynamoSession.User.UserName).Select(r => new UserTweetDto(r.Text, r.Timestamp, r.UserName));
                return result.ToList();
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
        
        public void Post(Tweet tweet)
        {
            tweet.UserName = DynamoSession.User.UserName;
            tweet.Timestamp = DateTime.Now.Ticks;
            Service.CreateTweet(tweet);
        }
    }
}
