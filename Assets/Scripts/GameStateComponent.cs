using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum State
{
    PLAYING,
    WAITING_ROOM,
    JOIN_ROOM,
    //PAUSE
};


public class GameStateComponent : MonoBehaviour
{
    public PlayerControllerComponent playerControllerComponent;
    public GraphGeneratorScript graphGenerator;

    public GameObject currentRouter;

    CameraFollowComponent mainCameraFollow;

    private State currentState;
    public State CurrentState { get {return currentState; }}

    List<GameObject> currentLayer;

    public GameObject countdownText;
    public Canvas waitingRoomCanvas;
    public GameObject joinRoomUI;
    void Start()
    {
        currentState = State.JOIN_ROOM;
        waitingRoomCanvas.enabled = false;


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
        // if (currentState == State.PLAYING)
        // {
        //     PlayLoop();
        //     return;
        // }

        // if (CurrentState == State.JOIN_ROOM)
        // {

        // }

        if (CurrentState == State.WAITING_ROOM)
        {
            WaitingRoomLoop();
            return;
        }
    }

    // private void PlayLoop()
    // {
    // }
    static public float maxWaitingSec = 15;
    private float waitingTimeRemaining = maxWaitingSec;
    private void WaitingRoomLoop()
    {
        // keep collecting players and assign numbers and colors and stuff
        if (waitingTimeRemaining > 0 && currentState == State.WAITING_ROOM)
        {
            waitingTimeRemaining -= Time.deltaTime;
            DisplayTime(waitingTimeRemaining);
            return;
        }

        if (currentState == State.WAITING_ROOM)
        {
            StartPlaying();
        }
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
        currentState = State.WAITING_ROOM;
    }
    public void StartPlaying()
    {
        // TODO hanlde "Play!" button signal
        if (currentState == State.PLAYING) {
            Debug.LogWarning("already playing!");
        }

        Debug.Log("Start Playing!!!");

        currentState = State.PLAYING;

        waitingRoomCanvas.enabled = false;

        // Generate Level
        graphGenerator.generateInitialLayers();

        // Assign randomly one router to this player
        // probably need to change with multyplayer, not random but sorted with player id
        currentLayer = graphGenerator.getLayer(0);
        currentRouter = currentLayer[Random.Range(0, currentLayer.Count)];
        playerControllerComponent.SetStartRouter(currentRouter.GetComponent<RouterComponent>());

        Debug.Log("current router " + currentRouter.name);

        mainCameraFollow = Camera.main.GetComponent<CameraFollowComponent>();
        if (mainCameraFollow != null)
        {
            mainCameraFollow.Follow(currentRouter.transform.position);
        }

    }





}
