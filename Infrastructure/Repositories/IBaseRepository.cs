using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly ApplicationDbContext _applicationDbContext;


    protected BaseRepository()
    {
    }
    protected BaseRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public IEnumerable<T> GetAll()
    {
        return _applicationDbContext.Set<T>().ToList();
    }

    public T? GetById(int id)
    {
        return _applicationDbContext.Set<T>().Find(id);
    }

    public int Add(T entity)
    {
        _applicationDbContext.Set<T>().Add(entity);
        return _applicationDbContext.SaveChanges();
    }

    public int Update(T entity)
    {
        _applicationDbContext.Set<T>().Entry(entity).State = EntityState.Modified;
        return _applicationDbContext.SaveChanges();
    }

    public int? Delete(int id)
    {
        T? entity = _applicationDbContext.Set<T>().Find(id);
        if (entity == null)
        {
            return null;
        }
        _applicationDbContext.Set<T>().Remove(entity);
        return _applicationDbContext.SaveChanges();
    }
}