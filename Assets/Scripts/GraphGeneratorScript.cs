using System.Linq;
using static System.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GraphGeneratorScript : MonoBehaviour
{
    public int minNumRoutersPerLayer, maxNumRoutersPerLayer;
    public int minDegree, maxDegree;

    public int windowSize;

    public float xSpaceRouters, ySpaceRouters;
    public int numVisibleLayers;

    public int shuffleAccuracy = 10000;

    public GameObject routerPrefab;

    List<GameObject> generateLayer(float x, float minY, float maxY, int numRouters) {
        List<GameObject> layer = new List<GameObject>();
    
        float y = minY, stepY = (maxY - minY) / (numRouters - 1);
        for(int i = 0; i < numRouters; i++) {
            GameObject router = Instantiate(routerPrefab, new Vector3(x, y, 0), Quaternion.identity);
            router.name = "x" + x + " y" + y;
            layer.Add(router);
            y += stepY;
        }
        
        return layer;
    }

    void addSingleLink(GameObject routerFrom, GameObject routerTo) {
        RouterComponent routerComponent = routerFrom.GetComponent<RouterComponent>();
        routerComponent.addLink(routerTo);
    }

    void generateLinksBetweenLayers(List<GameObject> layerFrom, List<GameObject> layerTo) {        
        int centerIdx = 0, stepIdx = (layerTo.Count + layerFrom.Count - 1) / layerFrom.Count;
        foreach(var routerFrom in layerFrom)
        {
            int mnIdx = Max(centerIdx - windowSize, 0);
            int mxIdx = Min(centerIdx + windowSize, layerTo.Count - 1);

            if(mnIdx <= mxIdx) {    
                int degree = Random.Range(minDegree, maxDegree);
                degree = Min(degree, mxIdx - mnIdx + 1);
                
                List<GameObject> randomChoice = new List<GameObject>();
                for(int i = mnIdx; i <= mxIdx; i++) {
                    randomChoice.Add(layerTo[i]);
                }

                randomChoice.Shuffle(shuffleAccuracy);
                if(degree < randomChoice.Count) {
                    randomChoice.RemoveRange(degree, randomChoice.Count - degree);
                }

                foreach(var routerTo in randomChoice) {
                    addSingleLink(routerFrom, routerTo);
                }
            }

            centerIdx += stepIdx;
        }
    }

    // public float linkWidth = 0.02f;
    void renderSingleLink(Vector3 posFrom, Vector3 posTo) {
                        
        // Set points
        lineRenderer.positionCount += 2;
        lineRenderer.SetPosition(lineRenderer.positionCount - 2, posFrom); 
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, posTo);
    }

    void renderLinks(List<GameObject> layer) {
        foreach (var routerFrom in layer) {
            RouterComponent routerComponent = routerFrom.GetComponent<RouterComponent>();
            foreach (var routerTo in routerComponent.getOutgoing()) {
                Vector3 posFrom = routerFrom.transform.position;
                Vector3 posTo = routerTo.transform.position;

                renderSingleLink(posFrom, posTo);
            }
        }
    }

    List<List<GameObject>> layers;

    public LineRenderer lineRenderer;
    void Start()
    {
        // generateInitialLayers();
        lineRenderer.positionCount = 0;
    }

    public void generateInitialLayers()
    {
        layers = new List<List<GameObject>>();

        float x = 0;
        float yMaxRouters = (maxNumRoutersPerLayer - 1) * ySpaceRouters;
        for (int i = 0; i < numVisibleLayers; i++) 
        {
            int numRouters = Random.Range(minNumRoutersPerLayer, maxNumRoutersPerLayer);

            float gap = 0.5f * (yMaxRouters -  (yMaxRouters / maxNumRoutersPerLayer) * numRouters);
            float minY = gap;
            float maxY = yMaxRouters - gap; 

            List<GameObject> layer = generateLayer(x, minY, maxY, numRouters); 

            if(i > 0) {
                generateLinksBetweenLayers(layers.Last(), layer);
                renderLinks(layers.Last());
            }

            foreach (var r in layer)
            {
                r.name = "Router L" + i + " " + r.name;
            }

            layers.Add(layer);
            x += xSpaceRouters;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getLayersCount()
    {
        // TODO check index is valid
        return layers.Count;
    }
    public List<GameObject> getLayer(int i)
    {
        // TODO check index is valid
        return layers[i];
    }
}
