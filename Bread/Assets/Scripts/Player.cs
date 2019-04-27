﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;            

    void Start()
    {
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void Update()
    {
    }

    private void MoveCharacter() 
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0);
        
        transform.position += movement * speed * Time.deltaTime;
        // transform.Translate(moveHorizontal * speed, moveVertical * speed, 0);

    }
}
