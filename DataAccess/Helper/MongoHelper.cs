using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Helper
{
    public class MongoHelper
    {
        private static readonly MongoConnectionStringBuilder ConnectionStringBuilder;

        static MongoHelper()
        {
            ConnectionStringBuilder = new MongoConnectionStringBuilder(Configuration.ConnectionString);
        }

        public static MongoServer GetServer(string connectionString)
        {
            var server = MongoServer.Create(connectionString);
            return server;
        }

        public static MongoServer GetServer()
        {
            var server = MongoServer.Create(ConnectionStringBuilder);
            return server;
        }

        public static MongoDatabase GetDatabase(string databaseName)
        {
            return GetServer().GetDatabase(databaseName);
        }

        public static MongoDatabase GetDatabase()
        {
            return GetDatabase(ConnectionStringBuilder.DatabaseName);
        }
    }
}
