using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Web.App.Model.DDB
{
    [DynamoDBTable(AppBootstrapper.TweetsTable)]
    public class Tweet
    {
        [DynamoDBHashKey]
        public string UserName { get; set; }
        
        [DynamoDBRangeKey]
        public long Timestamp { get; set; }

        [DynamoDBProperty]
        public string Text { get; set; }
    }
}