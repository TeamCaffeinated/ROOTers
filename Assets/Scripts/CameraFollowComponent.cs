using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowComponent : MonoBehaviour
{
    // public Transform player;
    private bool needToMove = false;
    public Vector3 nextPos;

    void Start()
    {
        GameEventsHandler.current.onMoveToNextRouter += OnMoveToNextRouter;
    }

    public void Follow(Vector3 v)
    {
        nextPos = new Vector3(v.x, v.y, transform.position.z);
        needToMove = true;
    }

    private void OnMoveToNextRouter(Vector3 routerPos)
    {
        Follow(routerPos);
    }

    void Update ()
    {
        if (needToMove)
        {
            // transform.position = player.transform.position + new Vector3(0, 1, -5);
            // transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = nextPos;
            needToMove = false;
        }
    }
}
