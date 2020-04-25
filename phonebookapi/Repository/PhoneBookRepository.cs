using System;
using PhonebookApi.Models;

namespace PhonebookApi.Repository
{
    public class PhoneBookRepository : BaseRepository<PhoneBook>, IPhoneBookRepository
    {
        public PhoneBookRepository(IMongoPhoneBookDBContext context) : base(context)
        {
        }
    }
}
