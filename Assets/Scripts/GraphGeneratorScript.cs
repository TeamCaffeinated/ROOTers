using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// using Router;

public class GraphGeneratorScript : MonoBehaviour
{
    public GameObject prefab;

    // Router lol;
    
    void Start()
    {
        for (int i = 0; i < 10; i++) 
        {
            var position = new Vector3(Random.Range(0,10),Random.Range(0,10),0);
            Instantiate(prefab, position, Quaternion.identity);
            // Debug.Log("x " + lol.x);
            // Debug.Log("y " + lol.y);
            // Console.WriteLine(i);
        }

        Debug.Log(prefab);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
