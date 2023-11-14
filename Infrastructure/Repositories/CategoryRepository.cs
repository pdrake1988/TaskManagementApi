using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}