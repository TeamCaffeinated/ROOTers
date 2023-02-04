using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

// using RouterComponent;

[System.Serializable]
public class RouterClickedEvent : UnityEvent // <RouterComponent>
{
    // public RouterComponent routerComponentClickedOn;
}

public class PlayerControllerComponent : MonoBehaviour
{
    public bool is_local_player = false;

    public RouterClickedEvent routerClickedEvent; // = new RouterClickedEvent();

    void Start()
    {
        if (routerClickedEvent == null)
            routerClickedEvent = new RouterClickedEvent();

        if (IsLocalPlayer())
        {
            // routerClickedEvent.AddListener(OnRouterClickedOn);
            routerClickedEvent.AddListener(OnRouterClickedOn);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

        if (is_local_player) 
        {
            if (Input.GetButtonDown("Vertical"))
            {
                Debug.Log("got vertical");
                Debug.Log(Input.GetAxis("Vertical"));
            }
        }
    }

    public bool IsLocalPlayer() {
        return is_local_player;
    }

    // public void OnPointerClick(PointerEventData eventData)
    // {
    //     RouterComponent router = GetComponent<RouterComponent>();

    //     
    //     Debug.Log("from router " + router.GetInstanceID() + "to router " + "TODO");
    //     Debug.Log("from router " + router.GetInstanceID() + "to router " + "TODO");
    // }

    void OnMouseDown()
    {
        // if (!IsLocalPlayer())
        if (true)
        {

            RouterComponent rc = GetComponent<RouterComponent>();
            // routerClickedEvent.routerComponentClickedOn = rc;
            // routerClickedEvent.Invoke(rc);
            routerClickedEvent.Invoke();
        }
        // PlayerControllerComponent pcc = GetComponent<PlayerControllerComponent>();
        // if (pcc.IsLocalPlayer()) {
        //     Gizmos.DrawLine(
        //         transform.position,

        //         new Vector3(-45.49318f,11.54467f,-69.10096f));
        //     Debug.Log("Sprite Clicked");
        // }
    }

    static public void OnUpdate()
    {

    }

    // public void OnRouterClickedOn(RouterComponent rc)
    public void OnRouterClickedOn()
    {
        if (IsLocalPlayer())
        // if (true)
        {
            // Debug.Log("Clicked on router " + rc);
            Debug.Log("Clicked on router ");
        }
    }

}
