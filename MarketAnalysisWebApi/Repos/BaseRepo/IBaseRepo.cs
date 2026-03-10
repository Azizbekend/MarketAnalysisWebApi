namespace MarketAnalysisWebApi.Repos.BaseRepo
{
    public interface IBaseRepo<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<ICollection<T>> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
