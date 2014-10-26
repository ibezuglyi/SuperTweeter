using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Web.App.Model.DDB
{
    [DynamoDBTable(AppBootstrapper.UsersTable)]
    public class User
    {
        [DynamoDBHashKey]
        public string UserName { get; set; }

        [DynamoDBProperty]
        public string Password { get; set; }

        [DynamoDBProperty]
        public string FullName { get; set; }

        [DynamoDBProperty]
        public List<string> Followers { get; set; }

        public User()
        {
            Followers = new List<string>();
        }
    }
}