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
    public Canvas waitingRoomCanvas;
    private List<GameObject> playerControllersList;

    void Start()
    {
        currentState = State.JOIN_ROOM;
        waitingRoomCanvas.enabled = false;

        playerControllersList = new List<GameObject>();


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
        }


        if (CurrentState == State.WAITING_ROOM)
        {
            WaitingRoomLoop();
            return;
        }
    }

    public const float secToWaitBeforeMove = 3;
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

        playerControllersList.Add(
            Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity)
        );

        // TODO properly
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
        // TODO hanlde "Play!" button signal
        if (currentState == State.PLAYING) {
            Debug.LogWarning("already playing!");
            return;
        }

        Debug.Log("Start Playing!!!");

        currentState = State.PLAYING;

        waitingRoomCanvas.enabled = false;

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
