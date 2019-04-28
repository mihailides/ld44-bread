using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paparazzi : MonoBehaviour
{
    public float secondsBeforeRotate = 1;
    public float angleToMove = 90;
    public Component player;
    private float timer;
    private AStarPathfinding aStarPathfinding;
    
    
    void Start()
    {   
        timer = 0.0f;
        aStarPathfinding = GetComponent<AStarPathfinding>();
    }
    
    void Update()
    {
        aStarPathfinding.FindPath(transform.position, player.transform.position);
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
