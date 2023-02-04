using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedStarterController : MonoBehaviour
{
    public uint playerCnt;
    public float maxSlope;
    public float verticalMargin;
    public float splitTime;
    public float turnTime;
    public float firstDelta;
    public float secondDelta;
    public GameObject exampleLine;

    private bool hasSplit = false;
    private bool hasTurned = false;

    GameObject[] playerLines;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < playerCnt; i++) {
            playerLines[i] = Instantiate(exampleLine);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < splitTime) {
            foreach (var line in playerLines) {
                var lineRenderer = line.GetComponent<LineRenderer>();
                var sndPnt = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                sndPnt.x += firstDelta;
            }
        } else if (Time.time < turnTime) {
            if (!hasSplit) {
                foreach (var line in playerLines) {
                    var lineRenderer = line.GetComponent<LineRenderer>();
                    var sndPnt = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, sndPnt);
                }
                hasSplit = true;
            }

            for(int i = 0; i < playerCnt; i++) {
                var lineRenderer = playerLines[i].GetComponent<LineRenderer>();
                var thrdPnt = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                thrdPnt.y += secondDelta - 4 * i * secondDelta / playerCnt;
            }
        }
        
    }
}
