using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableLink : MonoBehaviour
{

    // public SpriteRenderer cableSprite;
    public GameObject cableSprite;
    public LineRenderer lineRenderer;


    private Transform startPoint, endPoint;
    public Transform StartPoint{ set { startPoint = value; needUpdate = true;} }
    public Transform EndPoint{ set { endPoint = value; needUpdate = true;} }

    private bool needUpdate = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startPoint == null || endPoint == null) {
            // Debug.Log("null things");
            return;
        }
        if (needUpdate) {
            // Debug.Log("updating");

            // float angle = 
            //     (endPoint.position.y - startPoint.position.y)/
            //     (endPoint.position.x - startPoint.position.x)
            // ;
            // cableSprite.transform.position = Vector3.Lerp(
            //     startPoint.position,
            //     endPoint.position,
            //     0.5f
            // );
            // cableSprite.transform.Rotate(new Vector3(0,0,1), angle);

            // var l = new LineRenderer(lineRenderer);


            Strech(cableSprite, startPoint.position, endPoint.position, false);


            needUpdate = false;
        }
    }

    public void Strech(GameObject _sprite,Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ) {
         Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
         _sprite.transform.position = centerPos;
         Vector3 direction = _finalPosition - _initialPosition;
         direction = Vector3.Normalize(direction);
         _sprite.transform.right = direction;
         if (_mirrorZ) _sprite.transform.right *= -1f;
         // Vector3 scale = new Vector3(1,1,1);
         Vector3 scale = transform.localScale;
         Debug.Log(scale);
         scale.x = Vector3.Distance(_initialPosition, _finalPosition);
         _sprite.transform.localScale = scale;
     }
}
