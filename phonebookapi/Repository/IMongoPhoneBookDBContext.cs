using System;
using MongoDB.Driver;

namespace PhonebookApi.Repository
{
    public interface IMongoPhoneBookDBContext
    {
        IMongoCollection<PhoneBook> GetCollection<PhoneBook>();
    }
}
