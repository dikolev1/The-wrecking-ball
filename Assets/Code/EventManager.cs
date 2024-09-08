using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent OnWin =  new();
    public static UnityEvent OnFail = new();

    public static void SendWin()
    {
        OnWin?.Invoke();
    }

    public static void SendFail()
    {
        OnFail?.Invoke();
    }
}
