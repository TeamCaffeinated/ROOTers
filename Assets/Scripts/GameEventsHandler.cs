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

    // Used to asses if next router is reachable and move onto it
    public event Action<RouterComponent> onNextRouterSelected;
    public void NextRouterSelected(RouterComponent rc)
    {
        if (onNextRouterSelected != null)
        {
            onNextRouterSelected(rc);
        }
    }

    public event Action<Vector3> onMoveToNextRouter;
    public void MoveToNextRouter(Vector3 routerPos)
    {
        if (onMoveToNextRouter != null)
        {
            onMoveToNextRouter(routerPos);
        }
    }
}
