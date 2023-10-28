using Task = Core.Entities.Task;

namespace Core.Interfaces;

public interface IBaseRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
    int Add(T entity);
    int Update(T entity);
    int? Delete(int id);
}