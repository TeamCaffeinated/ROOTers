using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coherence.Connection;
using Coherence.Toolkit;
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
    public CoherenceMonoBridge MonoBridge;
    public PlayerControllerComponent playerControllerComponent;
    public GraphGeneratorScript graphGenerator;

    public GameObject currentRouter;

    CameraFollowComponent mainCameraFollow;

    public uint currentState = (uint)State.JOIN_ROOM;
    public uint CurrentState { get {return currentState; }}

    List<GameObject> currentLayer;

    public GameObject countdownText;
    public Canvas waitingRoomCanvas;
    public GameObject joinRoomUI;

    public byte[] dummyArray;


    void Start()
    {
        currentState = (uint)State.JOIN_ROOM;
        waitingRoomCanvas.enabled = false;

        int[] intArray = {1,2,3,4};

        dummyArray = new byte[intArray.Length * sizeof(int)];
        Buffer.BlockCopy(intArray, 0, dummyArray, 0, dummyArray.Length);

        //public
        // var countdownText = GetComponent<Canvas>().GetComponent("CountdownText"); // as HingeJoint;

        // Canvas.FindObjectOfType<Button

        // public GameObject fooText;
        // and in the inspector drag the text into the slot. Then put

        // Text footext = fooText.GetComponent<Text>();
        // fooText.text = "Change the text";
        // print(countdownText.GetComponent<TMP_Text>());
        // print(countdownText.GetType());
    }
    

    // Update is called once per frame
    void Update()
    {
        if (currentState == (uint)State.PLAYING)
        {
            // PlayLoop();

            Debug.Log(dummyArray);

            int[] intArray = new int[dummyArray.Length * sizeof(byte)];

            Buffer.BlockCopy( dummyArray, 0, intArray, 0, intArray.Length);

            Debug.Log(graphGenerator.layersAsGameObject.GetComponent<RouterGraph>().layers.Count);

            return;
        }

        // if (CurrentState == State.JOIN_ROOM)
        // {

        // }

        if (CurrentState == (uint)State.WAITING_ROOM)
        {
            WaitingRoomLoop();
            return;
        }
    }

    // private void PlayLoop()
    // {
    // }
    static public float maxWaitingSec = 60 * 3;
    public float waitingTimeRemaining = maxWaitingSec;
    private void WaitingRoomLoop()
    {
        // keep collecting players and assign numbers and colors and stuff
        if (waitingTimeRemaining > 0 && currentState == (uint)State.WAITING_ROOM)
        {
            waitingTimeRemaining -= Time.deltaTime;
            DisplayTime(waitingTimeRemaining);
            HandlePlayerJoin();
            return;
        }

        if (currentState == (uint)State.WAITING_ROOM)
        {
            StartPlaying();
        }
    }

    private List<ClientID> otherClients;
    private void HandlePlayerJoin()
    {
        // Raised whenever a new connection is made (including the local one).
        MonoBridge.ClientConnections.OnCreated += connection =>
        {
            Debug.Log($"Connection #{connection.ClientId} " +
                      $"of type {connection.Type} created.");

            if (! connection.IsMyConnection)
            {
                otherClients.Add(connection.ClientId);
            }
            Debug.Log("All clients until now " + otherClients);
        };

        // // Raised whenever a connection is destroyed.
        // MonoBridge.ClientConnections.OnDestroyed += connection =>
        // {
        //     Debug.Log($"Connection #{connection.ClientId} " +
        //               $"of type {connection.Type} destroyed.");
        // };

        // Raised when all initial connections have been synced.
        MonoBridge.ClientConnections.OnSynced += connectionManager =>
        {
            Debug.Log($"ClientConnections are now ready to be used.");
        };

    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        string timestr = string.Format("{0:00}:{1:00}", minutes, seconds);
        Debug.Log(timestr);
        countdownText.GetComponent<TMP_Text>().text = timestr;
    }

    public void OnJoinRoom()
    {
        waitingRoomCanvas.enabled = true;
        currentState = (uint)State.WAITING_ROOM;
    }
    public void StartPlaying()
    {
        // TODO hanlde "Play!" button signal
        if (currentState == (uint)State.PLAYING) {
            waitingTimeRemaining = -1;
            Debug.LogWarning("already playing!");
        }

        Debug.Log("Start Playing!!!");

        currentState = (uint)State.PLAYING;

        waitingRoomCanvas.enabled = false;

        // Generate Level
        graphGenerator.generateInitialLayers();

        // Assign randomly one router to this player
        // probably need to change with multyplayer, not random but sorted with player id
        currentLayer = graphGenerator.getLayer(0);
        // currentRouter = currentLayer[Random.Range(0, currentLayer.Count)];
        currentRouter = currentLayer[0];
        playerControllerComponent.SetStartRouter(currentRouter.GetComponent<RouterComponent>());



        Debug.Log("current router " + currentRouter.name);

        mainCameraFollow = Camera.main.GetComponent<CameraFollowComponent>();
        if (mainCameraFollow != null)
        {
            mainCameraFollow.Follow(currentRouter.transform.position);
        }

    }





}
