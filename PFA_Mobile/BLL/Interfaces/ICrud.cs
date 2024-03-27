namespace BLL.Interfaces
{
    public interface ICrud<T> where T : class
    {
        /// <param name="itemDto">DTO сущности</param>
        /// <returns>
        ///     Возращает true при успешном создании экземпляра сущности T,
        ///     иначе false
        /// </returns>
        Task<bool> Create(T itemDto);

        /// <returns>
        ///     Возращает true при успешном удалении экзмпляра сущности T,
        ///     иначе false
        /// </returns>
        Task<bool> Delete(int id);

        /// <returns>
        ///     Возращает true, если экземпляр сущности с указанным id существует
        ///     иначе false
        /// </returns>
        Task<bool> Exists(int id);

        Task<List<T>> GetAll();

        /// <returns>
        ///     Возвращает экземпляр сущности,
        ///     если он найден по указанному id, иначе null
        /// </returns>
        Task<T> GetById(int id);

        /// <returns>
        ///     Возращает true при успешном обновлении экземпляра сущности,
        ///     иначе false
        /// </returns>
        Task<bool> Update(T itemDto);
    }
}
