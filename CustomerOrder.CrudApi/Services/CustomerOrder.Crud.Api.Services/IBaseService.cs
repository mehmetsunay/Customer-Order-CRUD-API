using CustomerOrder.CrudApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.Crud.Api.Services
{
    public interface IBaseService<TEntity>
            where TEntity : class, IBase
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> ReadAsync(int id, bool tracking = true);

        Task<TEntity> UpdateAsync(int id, TEntity updateEntity);

        Task DeleteAsync(int id);
    }
}
