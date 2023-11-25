namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();

        /// <returns>
        ///     Возвращает найденную сущность,
        ///     иначе null
        /// </returns>
        Task<T> GetItem(int id);
        Task Create(T item);
        Task Update(T item);
        Task<bool> Delete(int id);

        Task<bool> Exists(int id);
    }
}
