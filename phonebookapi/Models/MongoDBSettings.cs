using System;
namespace PhonebookApi.Models
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string PhoneBooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
    }

    public interface IMongoDBSettings
    {
        string PhoneBooksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}




