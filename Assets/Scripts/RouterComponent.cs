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

    void Start()
    {
        // spriteRenderer = GetComponentInParent<SpriteRenderer>();
        // particleSystem = GetComponentInParent<ParticleSystem>();
    }

    void Update()
    {

        
    }

    public void hasPlayer() // TODO change name
    {
        spriteRenderer.color = new Color(0,1,1,1);
    }

    // public void OnPointerClick(PointerEventData eventData)
    void OnMouseDown()
    {
        // Debug.Log(eventData);
        // Debug.Log("from router " + GetInstanceID() + "to router " + "TODO");
        Debug.Log("clicked on " + name);
        // spriteRenderer.transform.localScale *= 2;
        particleSystem.Play();
    }

    // private bool CanReach(another router)

}
