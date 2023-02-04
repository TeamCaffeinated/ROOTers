using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class RouterComponent : MonoBehaviour // , IPointerClickHandler 
{
    // public int x,y;
    // public GameObject routerPrefab;

    public SpriteRenderer spriteRenderer;
    public new ParticleSystem particleSystem;

    List<GameObject> adjOut = new List<GameObject>();

    public List<GameObject> getOutgoing() {
        return adjOut;
    }

    public void addLink(GameObject adjRouter) {
        adjOut.Add(adjRouter);
    }

    public bool CanReach(RouterComponent targetRC) {
        foreach (GameObject adjR in adjOut)
        {
            RouterComponent adjRC = adjR.GetComponent<RouterComponent>();
            if (adjRC != null && adjRC == targetRC) {
                return true;
            }
        }
        return false;
    }


    private bool hasPlayer = false;
    public bool HasPlayer() { return hasPlayer; } // there should be a nicer way
    public void PlayerMovedIn()
    {
        hasPlayer = true;
        spriteRenderer.color = new Color(0,1,1,1);
    }
    public void PlayerMovedOut()
    {
        hasPlayer = false;
        spriteRenderer.color = new Color(1,1,1,1);
    }

    void OnMouseDown()
    {
        RespondToInteraction();
    }

    private void RespondToInteraction()
    {
        Debug.Log("clicked on " + name);
        particleSystem.Play();
        if (GameEventsHandler.current == null)
        {
            Debug.Log("dab init");
            return;
        }
        GameEventsHandler.current.NextRouterSelected(this);
    }

}
