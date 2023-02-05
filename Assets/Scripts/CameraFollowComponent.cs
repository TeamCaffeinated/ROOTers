using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraFollowComponent : MonoBehaviour
{
    // public Transform player;
    private bool needToMove = false;
    public Vector3 startPos,endPos;
    private bool madesound = false;
    private bool isfirstsound = true;

    void Start()
    {
        GameEventsHandler.current.onMoveToNextRouter += OnMoveToNextRouter;

       
    }

    public void Follow(float x_coord)
    {
        endPos = new Vector3(
            x_coord,
            transform.position.y,
            transform.position.z
            );
        startPos = transform.position;
        needToMove = true;
    }

    private void OnMoveToNextRouter(float x_coord)
    {
        Follow(x_coord);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/Slide");
    }

    float timeElapsed = 0;
    public float lerpDuration = 1.2f;
    
    void Update ()
    {
        if (needToMove)
        {

            // transform.position = player.transform.position + new Vector3(0, 1, -5);
            // transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            // transform.position = nextPos;

          
          


            if (timeElapsed < lerpDuration)
            {
                transform.position = Vector3.Lerp(startPos, endPos, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
            } else {
                transform.position = endPos;
                timeElapsed = 0;
                needToMove = false;
            }
        }
        else
        {
            madesound = false;
        }
    }
}
