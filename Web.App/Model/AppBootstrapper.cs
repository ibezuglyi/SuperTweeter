using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.App.Model
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    // creating tables must be done with low level api (IAmazonDynamoDB)

    public class AppBootstrapper
    {
        private IAmazonDynamoDB client;

        public const string UsersTable = "users3";
        public const string TweetsTable = "tweets3";
        public const string UserTweetsTable = "userTweets3";
        public const string UserChatTable = "userChat2";
        public const string MessageTable = "message2";

        public AppBootstrapper(IAmazonDynamoDB client)
        {
            this.client = client;
        }

        public void Create()
        {
            var tables = client.ListTables();

            if (!tables.TableNames.Contains(UsersTable))
            {
                CreateUserTable();
            }

            if (!tables.TableNames.Contains(TweetsTable))
            {
                CreateTweetsTable();
            }

            if (!tables.TableNames.Contains(UserTweetsTable))
            {
                CreateUserTweetsTable();
            }
        }

        private void CreateUserTable()
        {
            var userTableRequest = new CreateTableRequest();
            userTableRequest.TableName = UsersTable;

            userTableRequest.KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "UserName",
                        KeyType = KeyType.HASH
                    }
                };

            userTableRequest.AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "UserName",
                        AttributeType = ScalarAttributeType.S
                    }
                 
                };

            userTableRequest.ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 1, WriteCapacityUnits = 1 };

            this.client.CreateTable(userTableRequest);
        }

        private void CreateTweetsTable()
        {
            var tweetsTableRequest = new CreateTableRequest();
            tweetsTableRequest.TableName = TweetsTable;

            tweetsTableRequest.KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "UserName",
                    KeyType = KeyType.HASH
                },
                new KeySchemaElement
                {
                    AttributeName = "Timestamp",
                    KeyType = KeyType.RANGE
                }
            };

            tweetsTableRequest.AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition
                {
                    AttributeName = "UserName",
                    AttributeType = ScalarAttributeType.S
                },
           new AttributeDefinition
                {
                    AttributeName = "Timestamp",
                    AttributeType = ScalarAttributeType.N
                }
            };

            tweetsTableRequest.ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 1, WriteCapacityUnits = 1 };

            this.client.CreateTable(tweetsTableRequest);
        }

        private void CreateUserTweetsTable()
        {
            var userTweetsTableRequest = new CreateTableRequest();
            userTweetsTableRequest.TableName = UserTweetsTable;

            userTweetsTableRequest.KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "UserName",
                    KeyType = KeyType.HASH
                },
                new KeySchemaElement
                {
                    AttributeName = "Timestamp",
                    KeyType = KeyType.RANGE
                }
            };

            userTweetsTableRequest.AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition
                {
                    AttributeName = "UserName",
                    AttributeType = ScalarAttributeType.S
                },
           new AttributeDefinition
                {
                    AttributeName = "Timestamp",
                    AttributeType = ScalarAttributeType.N
                }
            };

            userTweetsTableRequest.ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 1, WriteCapacityUnits = 1 };

            this.client.CreateTable(userTweetsTableRequest);
        }
    }
}

