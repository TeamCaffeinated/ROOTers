using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

enum PlayerStatus {
    ALIVE,
    DYING,
    DEAD
}

public class PlayerControllerComponent : MonoBehaviour
{
    private PlayerStatus playerStatus = PlayerStatus.ALIVE;
    private int playerNumber;
    public int PlayerNumber{ get {return playerNumber;} }
    private RouterComponent currentRouter;

    public CableHighlighterComponent cableHighlighterComponent;

    public float health = 1.0f;

    private bool movedDown = false;
    private bool movedUp = false;

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

    public Color P1_Color;
    public Color P2_Color;
    public Color P3_Color;
    public Color P4_Color;
    public Color P5_Color;
    public Color P6_Color;
    public Color P7_Color;
    public Color P8_Color;

    List<Color> playerColors;
    Color currentPlayerColor;

    void Start()
    {
        IncreasePlayerCount();
        playerNumber = playerCount;

        name = "P" + playerNumber;

        Debug.Log("player count = " + playerCount + "playerNum = " + playerNumber);
        // GameEventsHandler.current.onNextRouterSelected += OnNextRouterSelected;
        GameEventsHandler.current.onMoveToNextRouter += OnMoveToNextRouter;

        playerColors = new List<Color> { P1_Color, P2_Color, P3_Color, P4_Color, P5_Color, P6_Color, P7_Color, P8_Color};
        currentPlayerColor = playerColors[playerNumber-1];
        // currentPlayerColor = new Color(1,0,1,1);
        // Debug.Log(currentPlayerColor);

        // cableHighlighterComponent.spriteRenderer.color = currentPlayerColor;
        cableHighlighterComponent.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);


        // lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (playerStatus == PlayerStatus.DEAD)
        {
            return;
        }

        // if (playerStatus == PlayerStatus.DYING)
        // {
        //     // PlayDeathAnimation();
        //     return;
        // }

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
                Die();
                return;
            }
            putativeNextRouter = currentRouter.getOutgoing()[putativeNextRouterIndex].GetComponent<RouterComponent>();
            return;
        }

        if (nextRouterSelected)
        {
            return;
        }

        // if (Input.GetButtonDown("Submit_P" + playerNumber))
        // {
        //     // GameEventsHandler.current.NextRouterSelected(playerNumber, putativeNextRouter);
        //     OnNextRouterSelected();
        //     return;
        // }

        float vertical = -Input.GetAxis("Vertical_P" + playerNumber);
        // Debug.Log("got vertical_P" + playerNumber + " " + vertical);
        if (vertical > Mathf.Epsilon)
        {
            if (!movedDown)
                putativeNextRouterIndex = (int)Mathf.Min(putativeNextRouterIndex+1, currentRouter.getOutgoing().Count-1);
            movedDown = true;
        }
        else if (vertical < -Mathf.Epsilon)
        {
            if (!movedUp)
                putativeNextRouterIndex = (int)Mathf.Max(putativeNextRouterIndex-1, 0);
            movedUp = true;
        } else {
            movedDown = false;
            movedUp = false;
        }
        Debug.Log(putativeNextRouterIndex);
        putativeNextRouter = currentRouter.getOutgoing()[putativeNextRouterIndex].GetComponent<RouterComponent>();

        cableHighlighterComponent.startPoint = currentRouter.transform;
        cableHighlighterComponent.endPoint   = putativeNextRouter.transform;
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

    private void Die()
    {
        // PlayDeathAnimation();
        playerStatus = PlayerStatus.DEAD;
        GameEventsHandler.current.OnPlayerDeath(playerNumber);
    }

    public float movementHealthCost = 0.2f;
    private void OnMoveToNextRouter(float y_coord)
    {
        currentRouter.PlayerMovedOut();
        currentRouter = putativeNextRouter;
        currentRouter.PlayerMovedIn();

        nextRouterSelected = false;
        putativeNextRouterIndex = 0;
        putativeNextRouter = null;

        health -= movementHealthCost;
        if (health < movementHealthCost) {
            Die();
        }
    }

}
