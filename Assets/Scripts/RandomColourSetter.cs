using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColourSetter : MonoBehaviour
{
    public Color cubeColor;

    public bool colorSet = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!colorSet) {
            cubeColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            colorSet = true;
        }
        
        transform.GetComponent<Renderer>().material.color = cubeColor;
    }
}
