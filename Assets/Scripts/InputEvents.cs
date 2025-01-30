using System;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
    public static event Action OnTouch;

    public static void InvokeOnTouch()
    {
        OnTouch?.Invoke();
    }
}
