using client.Model.Models;

namespace client.Model.Interfaces;

/// <summary>
/// Интерфейс сервиса бюджетов
/// </summary>
public interface IBudgetService
{
    /// <summary>
    /// Получает сальдо бюджета
    /// </summary>
    /// <param name="budget">Бюджет</param>
    /// <returns></returns>
    decimal? GetBalance(BudgetModel budget);
}