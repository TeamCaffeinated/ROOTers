using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouterComponent : MonoBehaviour
{
    // public int x,y;
    public GameObject routerPrefab;

    List<GameObject> adjOut = new List<GameObject>();

    public List<GameObject> getOutgoing() {
        return adjOut;
    }

    public void addLink(GameObject adjRouter) {
        adjOut.Add(adjRouter);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
