using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum State: uint
{
    PLAYING,
    WAITING_ROOM,
    JOIN_ROOM,

    WINNER,
    //PAUSE
};

// [System.Serializable]
// public struct PlayerList
// {
//     // public List<int> playerList;
//     public int playerId;
// }



public class GameStateComponent : MonoBehaviour
{
    public GameObject playerControllerPrefab;
    public GraphGeneratorScript graphGenerator;

    CameraFollowComponent mainCameraFollow;

    private State currentState;
    public State CurrentState { get {return currentState; }}

    private int currentLayerIndex = 0;

    public GameObject countdownText;
    public Canvas waitingRoomCanvas, playingUICanvas, winnerCanvas;

    // public GameObject roundComponent;
    private RoundEndComponent roundEndComponent;

    private List<GameObject> playerControllersList;

    void Start()
    {
        currentState = State.JOIN_ROOM;
        waitingRoomCanvas.enabled = false;
        playingUICanvas.enabled   = false;
        winnerCanvas.enabled      = false;

        playerControllersList = new List<GameObject>();

        roundEndComponent = GetComponent<RoundEndComponent>();


        GameEventsHandler.current.onPlayerDeath += OnPlayerDeath;

        OnJoinRoom();
    }
    

    void Update()
    {
        if (currentState == State.PLAYING)
        {
            PlayLoop();
            return;
        }

        // if (CurrentState == State.JOIN_ROOM)
        // {

        // }

        if (Input.GetButton("Submit"))
        {
            StartPlaying();
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/Start");
        }


        if (CurrentState == State.WAITING_ROOM)
        {
            WaitingRoomLoop();
            return;
        }
    }

    public float secToWaitBeforeMove = 3;
    private float waitedSec = 0;
    private void PlayLoop()
    {
        waitedSec += Time.deltaTime;
        if (waitedSec >= secToWaitBeforeMove)
        {
            currentLayerIndex = (int)Mathf.Min(currentLayerIndex+1, graphGenerator.getLayersCount() - 1);
            GameEventsHandler.current.MoveToNextRouter(
                graphGenerator.getLayer(
                    currentLayerIndex
                )[0].transform.position.x // ugly temp thing // need to do checks + abstractions for layer
            );
            waitedSec = 0;

            // Update health
            // roundComponent.GetComponent<RoundEndComponent>().onRoundEnd();
            roundEndComponent.onRoundEnd();
            ShowHealth();
        }

    }

    private void ShowHealth()
    {
        for (int i=0; i< playerControllersList.Count; i++) 
        {
            string targetName = "P" + (i+1) + " Life";
            foreach(var healthDisplayText in playingUICanvas.GetComponentsInChildren<TMP_Text>(true))
            {
                if(healthDisplayText.name == targetName)
                {
                    healthDisplayText.text = "P" + (i+1) + ": " + playerControllersList[i].GetComponent<PlayerControllerComponent>().health;
                }
            }
        }
    }

    static public float maxWaitingSec = 60 * 3;
    private float waitingTimeRemaining = maxWaitingSec;
    private void WaitingRoomLoop()
    {
        // keep collecting players and assign numbers and colors and stuff
        if (waitingTimeRemaining > 0 && currentState == State.WAITING_ROOM)
        {
            waitingTimeRemaining -= Time.deltaTime;
            DisplayTime(waitingTimeRemaining);
            CheckIfPlayerJoin();
            return;
        }

        if (waitingTimeRemaining <= 0 && currentState == State.WAITING_ROOM)
        {
            StartPlaying();
        }
    }
    void CheckIfPlayerJoin()
    {
        if (playerControllersList.Count >= 2)
        {
            return;
        }

        GameObject newPlayer = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        playerControllersList.Add(
            newPlayer
        );

        roundEndComponent.registerPlayer(
            newPlayer.GetComponent<PlayerControllerComponent>()
        );
        // TODO properly
    }

    void OnPlayerDeath(int playerNum)
    {
        for (int i=0; i<playerControllersList.Count; i++)
        {
            GameObject p = playerControllersList[i];
            if (p.name == "P" + playerNum)
            {
                roundEndComponent.removePlayer(
                    p.GetComponent<PlayerControllerComponent>()
                );
                playerControllersList.RemoveAt(i);
                Destroy(p);
                i--; // to reconsider this elem
            }
        }

        if (playerControllersList.Count == 1)
        {
            GameObject p = playerControllersList[0];
            ProclaimWinner(p);
        }
    }

    private void ProclaimWinner(GameObject p)
    {
        if (currentState == State.PLAYING)
        {
            // TODO "you win" screen
            Debug.Log("Player " + p.name + " WON!!");
            currentState = State.WINNER;

            waitingRoomCanvas.enabled = false;
            playingUICanvas.enabled   = false;
            winnerCanvas.enabled      = true;

            GameEventsHandler.current.OnPlayerWin(p.GetComponent<PlayerControllerComponent>().PlayerNumber);
        }
    }
    

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        string timestr = string.Format("{0:00}:{1:00}", minutes, seconds);
        // Debug.Log(timestr);
        countdownText.GetComponent<TMP_Text>().text = timestr;
    }

    public void OnJoinRoom()
    {
        waitingRoomCanvas.enabled = true;
        currentState = State.WAITING_ROOM;
    }
    public void StartPlaying()
    {
        // TODO add start animation
        // TODO hanlde "Play!" button signal
        if (currentState == State.PLAYING) {
            Debug.LogWarning("already playing!");
            return;
        }

        Debug.Log("Start Playing!!!");

        currentState = State.PLAYING;

        waitingRoomCanvas.enabled = false;
        playingUICanvas.enabled   = true;
        winnerCanvas.enabled      = false;

        graphGenerator.generateInitialLayers();

        for (int i=0; i<playerControllersList.Count; i++)
        {
            GameObject currentRouter = graphGenerator.getLayer(0)[i];
            PlayerControllerComponent currentPCC = playerControllersList[i].GetComponent<PlayerControllerComponent>();
            currentPCC.SetStartRouter(currentRouter.GetComponent<RouterComponent>());

            Debug.Log("Player " + currentPCC.PlayerNumber + " starting on router " + currentRouter.name);

            if (i == 0)
            {
                mainCameraFollow = Camera.main.GetComponent<CameraFollowComponent>();
                if (mainCameraFollow != null)
                {
                    Vector3 v = mainCameraFollow.transform.position;
                    mainCameraFollow.Follow(currentPCC.transform.position.x);
                        
                }
            }
        }



    }

}
