using System;
using PhonebookApi.Repository;
using PhonebookApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PhonebookApi.Repository
{
    public class MongoPhoneBookDBContext : IMongoPhoneBookDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        private string table { get; set; }
        public MongoPhoneBookDBContext(IOptions<MongoDBSettings> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.ConnectionString);
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
            table = configuration.Value.PhoneBooksCollectionName;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _db.GetCollection<T>(table);
        }
    }
}
