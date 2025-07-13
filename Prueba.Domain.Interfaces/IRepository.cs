namespace Prueba.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IQueryable<T> Get();
        T Add(T entity);
        T Update(int id, T entity);
        bool Delete(int id);
    }
}
