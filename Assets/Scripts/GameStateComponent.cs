using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateComponent : MonoBehaviour
{
    public PlayerControllerComponent playerControllerComponent;
    public GraphGeneratorScript graphGenerator;

    public GameObject currentRouter;

    CameraFollowComponent mainCameraFollow;

    List<GameObject> currentLayer;
    void Start()
    {
        graphGenerator.generateInitialLayers();
        currentLayer = graphGenerator.getLayer(0);

        currentRouter = currentLayer[Random.Range(0, currentLayer.Count)];

        playerControllerComponent.SetStartRouter(currentRouter.GetComponent<RouterComponent>());
        Debug.Log("current router " + currentRouter.name);

        mainCameraFollow = Camera.main.GetComponent<CameraFollowComponent>();
        if (mainCameraFollow != null)
        {
            mainCameraFollow.Follow(currentRouter.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
