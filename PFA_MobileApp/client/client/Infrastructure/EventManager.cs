namespace client.Infrastructure;

public static class EventManager
{
    public delegate Task TaskDelegate();
    public static event TaskDelegate OnUserExit;

    public static Task UserExitHandler() => OnUserExit?.Invoke();
}