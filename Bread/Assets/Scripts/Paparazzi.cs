using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paparazzi : MonoBehaviour
{
    public float secondsBeforeRotate = 1;
    public float angleToMove = 90;
    private float timer;

    void Start()
    {   
        timer = 0.0f;
    }

    void FixedUpdate() 
    {
        timer += Time.deltaTime;
        if (timer > secondsBeforeRotate) 
        {
            timer -= secondsBeforeRotate;
            transform.Rotate(0, 0, angleToMove);
        }      
    }
}
