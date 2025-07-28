using System.Linq.Expressions;
using trial.Models;

namespace trial.Repositry.Base
{
    public interface IGenericRepository<T> where T : class
    {
        T FindById(int id);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);
        void Addone(T myItem);
        void UpdateOne(T myItem);
        void DeleteOne(T myItem);
    }
}
