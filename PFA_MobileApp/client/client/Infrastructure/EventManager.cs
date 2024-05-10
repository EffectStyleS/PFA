using client.Model.Models;

namespace client.Infrastructure;

public static class EventManager
{
    public delegate Task? TaskDelegate();
    public static event TaskDelegate? OnUserExit;
    public static Task? UserExitHandler() => OnUserExit?.Invoke();

    public delegate void StringDelegate(string login);
    public static event StringDelegate? OnUserLogin;
    public static void UserLoginHandler(string login) => OnUserLogin?.Invoke(login);

    public delegate void GoalDelegate(GoalModel goal);
    public static event GoalDelegate? OnGoalChange;
    public static void GoalChangeHandler(GoalModel goal) => OnGoalChange?.Invoke(goal);
}