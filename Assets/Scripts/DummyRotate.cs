using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRotate : MonoBehaviour
{
    public Vector3 myRotation;
    public float speed = 1f;

    void Start()
    {
        myRotation = new Vector3(Random.value - 0.5f, Random.value  - 0.5f, Random.value - 0.5f) / 2;
    }

    void Update()
    {
        // Rotate an arbitrary amount
        transform.transform.Rotate(myRotation);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
    
        var spf = speed * Time.deltaTime;

        transform.position += new Vector3((h * spf), (v * spf), 0);
    }
}
