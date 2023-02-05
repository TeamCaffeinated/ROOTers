using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableHighlighterComponent : MonoBehaviour
{
    public Transform startPoint, endPoint;
    public SpriteRenderer spriteRenderer;

    private float interpAmount;
    void Start()
    {
        
    }

    void Update()
    {
        interpAmount = (interpAmount + Time.deltaTime) % 1f;
        spriteRenderer.transform.position = Vector3.Lerp(
            startPoint.position,
            endPoint.position,
            interpAmount
            );
    }
}
