using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;


public class PlayerControllerComponent : MonoBehaviour
{
    private RouterComponent currentRouter;
    void Start()
    {
        GameEventsHandler.current.onNextRouterSelected += OnNextRouterSelected;
    }

    public void SetStartRouter(RouterComponent rc)
    {
        if (currentRouter != null)
        {
            Debug.LogWarning("Start router already set once, use the funcion to move to next object instead!");
            return;
        }
        currentRouter = rc;
        rc.PlayerMovedIn();
    }

    private void OnNextRouterSelected(RouterComponent rc)
    {
        string s = "from " + currentRouter.name + " to " + rc.name;
        if (currentRouter.CanReach(rc)) {
            s += "CAN";
            currentRouter.PlayerMovedOut();
            currentRouter = rc;
            currentRouter.PlayerMovedIn();

        } else {
            s += "CAN'T";
        }
        Debug.Log(s);
    }

}
