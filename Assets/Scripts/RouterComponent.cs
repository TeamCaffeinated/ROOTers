using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class RouterComponent : MonoBehaviour // , IPointerClickHandler 
{
    // public int x,y;
    // public GameObject routerPrefab;

    public GameObject CilinderPrefab;
    // public SpriteRenderer spriteRenderer;
    // public new ParticleSystem particleSystem;

    List<GameObject> adjOut = new List<GameObject>();

    public float bloopMaxScaleFactorDelta = 0.015f;
    public float bloopFrequencyFactor = 30.0f;

    public int maxRouterPlayerCount = 4;
    public void Start() {
        InitSprite();
    }
    private void InitSprite() {
        GameObject c = Instantiate(
            CilinderPrefab,
            transform.position,
            // Vector3.zero,
            Quaternion.identity
            // transform
            );
        // c.transform.parent = transform;
        // Debug.Log("RouterComponent transform.position" + transform.position);
        // Debug.Log("c.transform.position" + c.transform.position);

        Vector3 offset = c.GetComponent<Cilinder>().nextDiskPosition.transform.position;
        for (int i=1; i<maxRouterPlayerCount; i++)
        {
            c = Instantiate(
                CilinderPrefab,
                // c.transform.position + offset,
                offset,
                // Vector3.zero,
                // new Vector3(1,1,0),
                Quaternion.identity
                // c.GetComponent<Cilinder>().nextDiskPosition
                );
            offset = c.GetComponent<Cilinder>().nextDiskPosition.transform.position;
            // c.transform.parent = transform;
        }
    }

    public void Update() {
        if (bloopAnimationRemainingTime > 0)
        {
            // spriteRenderer.GetComponent<Sprite>().texture.;
            bloopAnimationRemainingTime -= Time.deltaTime;
            //float val = (float)(Math.Sin((1 - bloopAnimationRemainingTime / maxBloopAnimationRemainingTime)*Math.PI/2.0f - Math.PI/2.0f));
            float val = (float)(Math.Sin(bloopAnimationRemainingTime * bloopFrequencyFactor) + 1) *bloopMaxScaleFactorDelta + 1;
            // * (1 - bloopMaxScaleFactorDelta) * (1.3f - bloopAnimationRemainingTime/maxBloopAnimationRemainingTime);

            // val = Math.Min(val, 0.9f);

            // Debug.Log(val + " " + bloopAnimationRemainingTime);

            transform.localScale = new Vector3(val,val,1);

            if (bloopAnimationRemainingTime < 0)
            {
                transform.localScale = new Vector3(1,1,1);
            }
        }
    }

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

    public Color withPlayerColor = new Color(0,1,1,1), defaultColor = new Color(1,1,0,1);
    public Color withBonusColor = new Color(1,0,1,1);

    public float bonus = 0;

    public void setBonus(float generatedBonus) {
        bonus = generatedBonus;
        if(bonus > 0) {
            // TODO
            // spriteRenderer.color = withBonusColor;
        }
    }

    public float getBonus() {
        return bonus;
    }   

    public void PlayerMovedIn()
    {
        hasPlayer = true;
        // TODO
        //  spriteRenderer.color = withPlayerColor;
    }
    public void PlayerMovedOut() 
    {        
        hasPlayer = false;
        // Ensure that defaultColor is non-bonus
        // TODO
        // spriteRenderer.color = defaultColor;
    }

    void OnMouseDown()
    {
        // RespondToInteraction();
    }

    // private void RespondToInteraction()
    // {
    //     Debug.Log("clicked on " + name);
    //     // particleSystem.Play();
    //     if (GameEventsHandler.current == null)
    //     {
    //         Debug.Log("bad init");
    //         return;
    //     }
    //     GameEventsHandler.current.NextRouterSelected(player, this);

    //     StartBloopAnimation();
    // }

    public float maxBloopAnimationRemainingTime = 0.0001f;
    private float bloopAnimationRemainingTime = 0.0f;
    private void StartBloopAnimation()
    {
        bloopAnimationRemainingTime = maxBloopAnimationRemainingTime;
    }

}
