using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateComponent : MonoBehaviour
{
    public PlayerControllerComponent playerControllerComponent;
    public GraphGeneratorScript graphGenerator;

    public GameObject currentRouter;

    List<GameObject> currentLayer;
    void Start()
    {
        graphGenerator.generateInitialLayers();
        currentLayer = graphGenerator.getLayer(0);

        currentRouter = currentLayer[Random.Range(0, currentLayer.Count)];
        currentRouter.GetComponent<RouterComponent>().hasPlayer();
        // currentRouter.GetComponent<Texture2D>().
        Debug.Log("current router " + currentRouter.name);
        // currentRouter.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
