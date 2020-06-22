using Microsoft.Extensions.Options;
using MongoDB.DataAccess.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbAspNetCore.DataAccess
{
    class CustomerContext : ICustContext
    {
        private IMongoDatabase mongoDatabase;
        public CustomerContext(IOptions<MySettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Customer> Customers => mongoDatabase.GetCollection<Customer>("Customers");
    }
    public interface ICustContext
    {
        IMongoCollection<Customer> Customers { get; }
    }
}
