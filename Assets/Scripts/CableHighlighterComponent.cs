using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableHighlighterComponent : MonoBehaviour
{
    public Transform startPoint, endPoint;
    public SpriteRenderer spriteRenderer;

    private float interpAmount;
    public float speed = 3.0f;
    void Start()
    {
        
    }

    void Update()
    {
        if (startPoint == null || endPoint == null) {
            return;
        }

        interpAmount = (interpAmount + Time.deltaTime * speed) % 1f;
        spriteRenderer.transform.position = Vector3.Lerp(
            startPoint.position,
            endPoint.position,
            interpAmount
            );
    }
}
