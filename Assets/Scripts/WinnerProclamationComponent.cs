using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerProclamationComponent : MonoBehaviour
{
    public GameObject winnerText;
    void Start()
    {
        GameEventsHandler.current.onPlayerWin += OnPlayerWin;
    }

    void OnPlayerWin(int playerNum)
    {
        winnerText.GetComponent<TMP_Text>().text = "Player " + playerNum + " won!!!";
    }
}
