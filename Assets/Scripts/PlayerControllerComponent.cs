using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;


public class PlayerControllerComponent : MonoBehaviour
{
    private int playerNumber;
    public int PlayerNumber{ get {return playerNumber;} }
    private RouterComponent currentRouter;

    public float health = 1.0f;
    public GameObject roundComponent;

    // public LineRenderer lineRenderer;

    static int playerCount = 0;
    static public void IncreasePlayerCount() { playerCount++; }
    // PlayerControllerComponent()
    // {
    //     IncreasePlayerCount();
    //     playerNumber = playerCount;
    // }

    public RouterComponent getCurrentRouter() {
        return currentRouter;
    }

    void Start()
    {
        IncreasePlayerCount();
        playerNumber = playerCount;
        Debug.Log("player count = " + playerCount + "playerNum = " + playerNumber);
        // GameEventsHandler.current.onNextRouterSelected += OnNextRouterSelected;
        GameEventsHandler.current.onMoveToNextRouter += OnMoveToNextRouter;

        roundComponent.GetComponent<RoundEndComponent>().registerPlayer(this);
        // lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.positionCount = 2;
    }

    void Update()
    {
        HandleController();
    }




    private int putativeNextRouterIndex = 0; // proper way, use just this
    private RouterComponent putativeNextRouter;
    private bool nextRouterSelected = false;
    void HandleController()
    {
        if (currentRouter == null)
        {
            return;
        }

        if (putativeNextRouter == null)
        {
            // Debug.Log("null putative");
            putativeNextRouterIndex = 0;
            if (currentRouter.getOutgoing().Count == 0)
            {
                Debug.Log("No exiting links from router");
            }
            putativeNextRouter = currentRouter.getOutgoing()[putativeNextRouterIndex].GetComponent<RouterComponent>();
            return;
        }

        if (nextRouterSelected)
        {
            return;
        }

        if (Input.GetButtonDown("Submit_P" + playerNumber))
        {
            // GameEventsHandler.current.NextRouterSelected(playerNumber, putativeNextRouter);
            OnNextRouterSelected();
            return;
        }

        float vertical = Input.GetAxis("Vertical_P" + playerNumber);
        Debug.Log("got vertical_P" + playerNumber + " " + vertical);
        if (vertical > 0)
        {
            putativeNextRouterIndex = (int)Mathf.Min(putativeNextRouterIndex+1, currentRouter.getOutgoing().Count-1);
        }
        if (vertical < 0)
        {
            putativeNextRouterIndex = (int)Mathf.Max(putativeNextRouterIndex-1, 0);
        }
        putativeNextRouter = currentRouter.getOutgoing()[putativeNextRouterIndex].GetComponent<RouterComponent>();

        // lineRenderer.SetPosition(0, currentRouter.transform.position); 
        // lineRenderer.SetPosition(1, putativeNextRouter.transform.position);
        //For creating line renderer object
        // LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        // lineRenderer.startColor = Color.yellow;
        // lineRenderer.endColor = Color.yellow;
        // lineRenderer.startWidth = 0.03f;
        // lineRenderer.endWidth = 0.03f;
        // lineRenderer.positionCount = 2;
        // lineRenderer.useWorldSpace = true;    


        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        Vector3 zOffset = new Vector3(0,0,0);

        // lineRenderer.positionCount += 2;
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, currentRouter.transform.position      + zOffset); 
        lineRenderer.SetPosition(1, putativeNextRouter.transform.position + zOffset);

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

    // private void OnNextRouterSelected(int pNum, RouterComponent rc)
    private void OnNextRouterSelected()
    {

        string s = "from " + currentRouter.name + " to " + putativeNextRouter.name;
        if (currentRouter.CanReach(putativeNextRouter)) {
            s += "CAN";

            nextRouterSelected = true;

            // GameEventsHandler.current.MoveToNextRouter(playerNumber, rc.transform.position);
            // GameEventsHandler.current.MoveToNextRouter(rc.transform.position);
        } else {
            s += "CAN'T";
        }
        Debug.Log(s);
    }

    private void OnMoveToNextRouter(float y_coord)
    {
        currentRouter.PlayerMovedOut();
        currentRouter = putativeNextRouter;
        currentRouter.PlayerMovedIn();

        nextRouterSelected = false;
        putativeNextRouterIndex = 0;
        putativeNextRouter = null;
    }

}
