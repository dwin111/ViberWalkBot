namespace ViberWalkBot.Repositories.Interface
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task<T> EditAsync(T entity);
    }
}
