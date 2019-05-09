using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{

    public Vector3 initialPos;
    public float speed = 0.5f; 
    Vector3 newPosition;

    void PositionChange()
    {
        newPosition = new Vector2(Random.Range(initialPos.x -5.0f, initialPos.x + 5.0f), Random.Range(-5.0f, 5.0f));
    }
   
    void Start()
    {   
        initialPos = transform.position;
        PositionChange();
    }

    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, newPosition) < 1)
            PositionChange();
 
        transform.position=Vector3.Lerp(transform.position,newPosition,Time.deltaTime*speed);
    }
}
