using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedStarterController : MonoBehaviour
{
    public uint playerCnt;
    public float verticalMargin;
    public float splitTime;
    public float turnTime;
    public float firstDelta;
    public float secondDelta;
    public float doneAcceleration;
    public float endTime;
    public GameObject exampleLine;
    public GameObject exampleRouter;

    public Color[] playerColors;

    private bool hasSplit = false;
    private bool hasTurned = false;
    private float doneSpeed = 0;

    GameObject[] playerLines;

    GameObject[] routers;

    // Start is called before the first frame update
    void Start()
    {
        playerLines = new GameObject[playerCnt];
        for (int i = 0; i < playerCnt; i++) {
            playerLines[i] = Instantiate(exampleLine);
            playerLines[i].transform.SetParent(transform);
            playerLines[i].SetActive(true);
            var line = playerLines[i].GetComponent<LineRenderer>();
            var grad = new Gradient();
            grad.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(playerColors[i], 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 1.0f), new GradientAlphaKey(1.0f, 1.0f) }
            );

            line.colorGradient = grad;
        }

        routers = new GameObject[playerCnt];
        for (int i = 0; i < playerCnt; i++) {
            routers[i] = Instantiate(exampleRouter);
            routers[i].SetActive(true);
            routers[i].GetComponent<SpriteRenderer>().color = playerColors[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        doneSpeed += doneAcceleration * Time.deltaTime;
        foreach (var line in playerLines) {
            var lineRenderer = line.GetComponent<LineRenderer>();
            var fstPnt = lineRenderer.GetPosition(0);
            fstPnt.x -= doneSpeed * Time.deltaTime;
            lineRenderer.SetPosition(0, fstPnt);

            for (int j = 1; j < lineRenderer.positionCount; j++) {
                lineRenderer.SetPosition(j, lineRenderer.GetPosition(j) - new Vector3(firstDelta * Time.deltaTime, 0, 0));
            }
        }

        if (Time.time < splitTime) {
            foreach (var line in playerLines) {
                var lineRenderer = line.GetComponent<LineRenderer>();
                var sndPnt = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                sndPnt.x += firstDelta * Time.deltaTime;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, sndPnt);
            }
            foreach (var router in routers)
                router.transform.Translate(new Vector3(-firstDelta * Time.deltaTime, 0, 0));
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
                thrdPnt.y += (playerCnt / 2f - i - 0.5f) * (secondDelta / playerCnt) * Time.deltaTime;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, thrdPnt);
            }
        } else {
            if (!hasTurned) {
                for (int i = 0; i < playerCnt; i++) {
                    var tmp = routers[i].transform.position;
                    var lineRenderer = playerLines[i].GetComponent<LineRenderer>();
                    var sndPnt = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                    tmp.y = sndPnt.y - 0.15f;
                    routers[i].transform.position = tmp;
                }

                foreach (var line in playerLines) {
                    var lineRenderer = line.GetComponent<LineRenderer>();
                    var sndPnt = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, sndPnt);
                }
                hasTurned = true;
            }
            foreach (var line in playerLines) {
                var lineRenderer = line.GetComponent<LineRenderer>();
                var sndPnt = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                sndPnt.x += firstDelta * Time.deltaTime;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, sndPnt);
            }

            if (Time.time < endTime)
                foreach (var router in routers)
                    router.transform.Translate(new Vector3(-firstDelta * Time.deltaTime, 0, 0));
        }
        
    }
}
