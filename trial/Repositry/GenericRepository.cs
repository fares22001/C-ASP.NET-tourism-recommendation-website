using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using trial.Data;
using trial.Models;
using trial.Repositry.Base;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected AppDbContext context;

    public GenericRepository(AppDbContext context)
    {
        this.context = context;
    }

    public T FindById(int id)
    {
        return context.Set<T>().Find(id);
    }

    public IEnumerable<T> FindAll()
    {
        return context.Set<T>().ToList();
    }

    public IEnumerable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = context.Set<T>();
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return query.ToList();
    }

    public void Addone(T myItem)
    {
        context.Set<T>().Add(myItem);
        context.SaveChanges();
    }

    public void UpdateOne(T myItem)
    {
        context.Set<T>().Update(myItem);
        context.SaveChanges();
    }

    public void DeleteOne(T myItem)
    {
        context.Set<T>().Remove(myItem);
        context.SaveChanges();
    }
}

