namespace PFA_Mobile_v2.Application.DatabaseInteraction.Interfaces;

/// <summary>
/// Интерфейс репозитория
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Возвращает все сущности данного типа
    /// </summary>
    Task<List<T>> GetAll();

    /// <returns>
    /// Возвращает найденную сущность, иначе null
    /// </returns>
    Task<T?> GetItem(int id);
    
    /// <summary>
    /// Создает сущность
    /// </summary>
    /// <param name="item">Сущность</param>
    Task Create(T item);
    
    /// <summary>
    /// Обновляет сущность
    /// </summary>
    /// <param name="item">Сущность с обновленными значениями</param>
    Task Update(T item);
    
    /// <summary>
    /// Удаляет сущность
    /// </summary>
    /// <param name="id">Id сущности</param>
    /// <returns>True - успешное удаление, иначе false</returns>
    Task<bool> Delete(int id);

    /// <summary>
    /// Проверяет наличие сущности с указанным Id
    /// </summary>
    /// <param name="id">Id сущности</param>
    /// <returns>True - сущность найдена, иначе false</returns>
    Task<bool> Exists(int id);
}