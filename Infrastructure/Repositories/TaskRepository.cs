using Core.Interfaces;
using Task = Core.Entities.Task;

namespace Infrastructure.Repositories;

public class TaskRepository : BaseRepository<Task>, ITaskRepository
{
    
}