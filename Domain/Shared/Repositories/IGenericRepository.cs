namespace Domain.Shared.Repositories;

public interface IGenericRepository<T>
{
  IQueryable<T> ListAll();
  Task<T?> ListById(Guid id);
  void Create(T entity);
  void Update(T entity);
  void Delete(T entity);
}