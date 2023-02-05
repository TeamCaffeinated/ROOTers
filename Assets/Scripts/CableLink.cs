using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableLink : MonoBehaviour
{

    public SpriteRenderer cableSprite;
    private Transform startPoint, endPoint;
    public Transform StartPoint{ set { startPoint = value;} }
    public Transform EndPoint{ set { endPoint = value;} }

    private bool needUpdate = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startPoint == null || endPoint == null) {
            return;
        }
        if (needUpdate) {
            float angle = 
                (endPoint.position.y - startPoint.position.y)/
                (endPoint.position.x - startPoint.position.x)
            ;
            // cableSprite.transform.RotateAround(new Vector3(0,0,1), angle);
            // cableSprite.transform.Rotate(new Vector3(0,0,1), angle);
            cableSprite.transform.Rotate(new Vector3(0,0,1), 45);
            needUpdate = false;
        }
    }
}
