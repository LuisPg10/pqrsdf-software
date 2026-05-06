using Domain.Generics;
using Domain.Shared.Repositories;

namespace Infraestructure.Shared.Repositories;

public class GenericRepository<T>(ApplicationDbContext db) : IGenericRepository<T> where T : GenericEntity
{
  private readonly DbSet<T> _dbSet = db.Set<T>();

  public virtual IQueryable<T> ListAll() => _dbSet.OrderBy(t => t.CreatedAt).AsNoTracking();
  public async Task<T?> ListById(Guid id) => await ListAll().FirstOrDefaultAsync(e => e.Id.Equals(id));
  public async void Create(T entity) => await _dbSet.AddAsync(entity);
  public void Update(T entity) => _dbSet.Update(entity);
  public void Delete(T entity) => _dbSet.Remove(entity);
}