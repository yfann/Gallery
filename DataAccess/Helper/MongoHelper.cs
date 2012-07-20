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
        private static readonly MongoConnectionStringBuilder ConnectionString;
        private static readonly MongoUrlBuilder ConnectionUrl;
        private static readonly bool IsConnectionUrl;

        static MongoHelper()
        {
            if (Configuration.ConnectionString.StartsWith("mongodb://"))
            {
                IsConnectionUrl = true;
                ConnectionUrl = new MongoUrlBuilder(Configuration.ConnectionString);
            }
            else
            {
                ConnectionString = new MongoConnectionStringBuilder(Configuration.ConnectionString);
            }
        }

        public static MongoServer GetServer(string connectionString)
        {
            var server = MongoServer.Create(connectionString);
            return server;
        }

        public static MongoServer GetServer()
        {
            MongoServer server = null;
            if (IsConnectionUrl)
            {
                server = MongoServer.Create(ConnectionUrl.ToMongoUrl());
            }
            else
            {
                server = MongoServer.Create(ConnectionString);
            }
            return server;
        }

        public static MongoDatabase GetDatabase(string databaseName)
        {
            return GetServer().GetDatabase(databaseName);
        }

        public static MongoDatabase GetDatabase()
        {
            if (IsConnectionUrl)
            {
                return GetDatabase(ConnectionUrl.DatabaseName);
            }
            else
            {
                return GetDatabase(ConnectionString.DatabaseName);
            }
        }
    }
}