using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhonebookApi.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Create(TEntity obj);
        void Update(Guid id,TEntity obj);
        void Delete(Guid id);
        Task<TEntity> Get(Guid id);
        Task<IEnumerable<TEntity>> Get();
    }
}
