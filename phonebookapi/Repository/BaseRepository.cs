using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PhonebookApi.Models;

namespace PhonebookApi.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoPhoneBookDBContext _mongoContext;

        protected IMongoCollection<TEntity> _dbCollection;

        protected BaseRepository(IMongoPhoneBookDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<TEntity> Get(Guid id)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", id);

            _dbCollection = _mongoContext.GetCollection<TEntity>();

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> Search(string name)
        {
            _dbCollection = _mongoContext.GetCollection<TEntity>();
            var nameFieldDefinition = new ExpressionFieldDefinition<PhoneBook, string>(x => x.Name);

            List<FilterDefinition<TEntity>> filters = new List<FilterDefinition<TEntity>>
            {
                Builders<TEntity>.Filter.Eq("Name", name),
            };

            var filterCombination = Builders<TEntity>.Filter.Or(filters);

            return await _dbCollection.FindAsync(filterCombination).Result.ToListAsync();
        }

        public void Create(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " is null");
            }
            _dbCollection = _mongoContext.GetCollection<TEntity>();
            _dbCollection.InsertOneAsync(obj);
        }

        public void Delete(Guid id)
        {
            _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
        }

        public void Update(Guid id, TEntity entity)
        {
            _dbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", id), entity);
        }


    }
}
