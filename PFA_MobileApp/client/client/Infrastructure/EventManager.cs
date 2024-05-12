using client.Model.Models;

namespace client.Infrastructure;

/// <summary>
/// Менеджер событий
/// </summary>
public static class EventManager
{
    /// <summary>
    /// Делегат с возвратным значением Task?
    /// </summary>
    public delegate Task? TaskDelegate();
    
    /// <summary>
    /// Событие выхода пользователя
    /// </summary>
    public static event TaskDelegate? OnUserExit;
    
    /// <summary>
    /// Обработчик события выхода пользователя
    /// </summary>
    public static Task? UserExitHandler() => OnUserExit?.Invoke();

    /// <summary>
    /// Делегат логина пользователя
    /// </summary>
    public delegate void LoginDelegate(string login);
    
    /// <summary>
    /// Событие логина пользователя
    /// </summary>
    public static event LoginDelegate? OnUserLogin;
    
    /// <summary>
    /// Обработчик событие логина пользователя
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    public static void UserLoginHandler(string login) => OnUserLogin?.Invoke(login);

    /// <summary>
    /// Делегат цели
    /// </summary>
    public delegate void GoalDelegate(GoalModel goal);
    
    /// <summary>
    /// Событие обновления цели
    /// </summary>
    public static event GoalDelegate? OnGoalChange;
    
    /// <summary>
    /// Обработчик события обновления цели
    /// </summary>
    /// <param name="goal">Цель</param>
    public static void GoalChangeHandler(GoalModel goal) => OnGoalChange?.Invoke(goal);
}