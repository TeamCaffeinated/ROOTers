using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsHandler : MonoBehaviour
{
    public static GameEventsHandler current;
    private void Awake()
    {
        current = this;
    }

    public event Action<RouterComponent> onNextRouterSelected;
    public void NextRouterSelected(RouterComponent rc)
    {
        if (onNextRouterSelected != null)
        {
            onNextRouterSelected(rc);
        }
    }
}
