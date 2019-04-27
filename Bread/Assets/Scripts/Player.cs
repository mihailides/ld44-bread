using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        
        var moving = (Math.Abs(moveHorizontal) > 0.1 || Math.Abs(moveVertical) > 0.1);
        animator.SetBool("moving", moving);
        
        transform.position += movement * speed * Time.deltaTime;
        // transform.Translate(moveHorizontal * speed, moveVertical * speed, 0);

    }
}
