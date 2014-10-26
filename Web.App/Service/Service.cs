using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Web.App.Model.DDB;

namespace Web.App.Service
{
    public class Service
    {
        private DynamoDBContext db;

        public Service(IAmazonDynamoDB client)
        {
            this.db = new DynamoDBContext(client);
        }

        public bool CreateUser(string userName, string password, string fullName)
        {
            var user = new User()
            {
                FullName = fullName,
                Password = password,
                UserName = userName,
            };
            user.Followers.Add(userName);
            try
            {
                db.Save<User>(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User GetUser(string userName)
        {
            var user = db.Load<User>(userName);
            return user;
        }

        //public List<UserTweets> GetTweetsByUserName(string userName)
        //{
        //    var userTweets = db.Query<UserTweets>(userName,
        //        QueryOperator.GreaterThanOrEqual,
        //        new List<object> { DateTime.Now.AddDays(-3).Ticks },    // get newer than 3 days
        //        new DynamoDBOperationConfig { BackwardQuery = true });  // order desc on RK

        //    return userTweets.ToList();
        //}

        public void CreateTweet(Tweet newTweet)
        {
            db.Save(newTweet);
            var user = GetUser(newTweet.UserName);

            var alltweets = user.Followers.Select(f => new UserTweet
            {
                Timestamp = newTweet.Timestamp,
                AuthorName = newTweet.UserName,
                UserName = f
            }).ToList();
            foreach (var userTweet in alltweets)
            {
                db.Save(userTweet);
            }


        }

        public List<Tweet> GetTweetsFromFollowedUsers(string userName)
        {
            var userTweets = db.Query<UserTweet>(userName,
                QueryOperator.GreaterThanOrEqual,
                new List<object> { DateTime.Now.AddDays(-3).Ticks },    // get newer than 3 days
                new DynamoDBOperationConfig { BackwardQuery = true });  // order desc on RK

            var tweetsBatch = db.CreateBatchGet<Tweet>();
            foreach (var userTweet in userTweets)
            {
                tweetsBatch.AddKey(userTweet.AuthorName, userTweet.Timestamp);
            }
            tweetsBatch.Execute();
            return tweetsBatch.Results;
        }

        public void AddFollowedUser(string userName, string followedUserName)
        {
            var user = GetUser(followedUserName);
            user.Followers.Add(userName);
            db.Save(user);
        }

        public List<string> GetAllUsers()
        {
            var query = db.Scan<User>();
            var user = GetUser(DynamoSession.User.UserName);
            var result = query.Where(r => !r.Followers.Contains(user.UserName)).Select(r => r.UserName).ToList();
            return result;
        }
    }
}