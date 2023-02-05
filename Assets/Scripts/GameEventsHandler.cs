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

    public event Action<float> onMoveToNextRouter;
    public void MoveToNextRouter(float x_coord)
    {
        if (onMoveToNextRouter != null)
        {
            onMoveToNextRouter(x_coord);
        }
    }

    public event Action<int> onPlayerDeath;
    public void OnPlayerDeath(int playerNum)
    {
        if (onPlayerDeath != null)
        {
            onPlayerDeath(playerNum);
        }
    }

    public event Action<int> onPlayerWin;
    public void OnPlayerWin(int playerNum)
    {
        if (onPlayerWin != null)
        {
            onPlayerWin(playerNum);
        }
    }
}
