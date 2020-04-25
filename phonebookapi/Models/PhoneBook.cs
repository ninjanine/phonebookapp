using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace phonebookapi.Models
{
    public class PhoneBook
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Entry> Entries { get; set; }   
    }
}
