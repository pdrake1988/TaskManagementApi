using Task = Core.Entities.Task;

namespace Core.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T?> GetById(int id);
    int Add(T entity);
    int Update(T entity);
    int? Delete(int id);
}