using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FujitsuDemoApp.App_Start
{
    public class MongoContext
    {
        MongoClient _client;
        public IMongoDatabase _database;
        public MongoContext()        //constructor 
        {
            // Reading credentials from Web.config file 
            var MongoConn = ConfigurationManager.AppSettings["MongoDatabaseConnectionString"]; //CarDatabase
            var MongoDBName = ConfigurationManager.AppSettings["MongoDBName"]; //demouse
            var client = new MongoClient(MongoConn);
            _database = client.GetDatabase(MongoDBName);
        }
    }
}