using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Repositorios
{
    public interface IRepository<T, K>
    {
        void Create(T entity);
        void CreateMultiple(IEnumerable<T> entities);
        T GetById(K id);
        void Update(T entity);
        IList<T> FindAll();
    }
}
