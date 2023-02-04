using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouterGeneratorBehaviour : MonoBehaviour
{
    public GameObject routerPrefab;
    public uint playerNumber;
    public uint playerCount;
    public uint visibleLayers;
    public uint meanRoutersPerLayer;
    public uint varianceRoutersPerLayer;
    public bool needNewLayer;
    public Vector3 topLeftRouterPos;
    public float spawnDeltaX;
    public float spawnDeltaY;

    void generateInitialRouters() {
        for (int i = 0; i < playerCount; i++) {
            GameObject tmp = Instantiate(routerPrefab);
            tmp.transform.position = topLeftRouterPos + (new Vector3(0, spawnDeltaY, 0)) * i;
            tmp.transform.parent = transform;
        }
    }

    void generateNextRouters() {

    }

    // Start is called before the first frame update
    void Start()
    {
        generateInitialRouters();
        for (int i = 0; i < visibleLayers; i++) {
            generateNextRouters();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (needNewLayer) {
            generateNextRouters();
        }
        
    }
}
