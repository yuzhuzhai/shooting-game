﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaryPlayerMove : MonoBehaviour {

    private Rigidbody2D myBody;
    private float moveForce_X = 2.0f, moveForce_Y = 2.0f;
    private bool isGround = true;

    private PlayerAnimations playerAnimation;

	void Awake () {
        myBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimations>();
	}

	void FixedUpdate () {
        Move();
	}

    void Move()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.RightArrow)) {
        
            myBody.velocity = new Vector2(moveForce_X, myBody.velocity.y);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            myBody.velocity = new Vector2(-moveForce_X, myBody.velocity.y);

        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            myBody.velocity = new Vector2(myBody.velocity.x, moveForce_Y);

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            myBody.velocity = new Vector2(myBody.velocity.x, -moveForce_Y);

        }
        else
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0f);
        }

          if ( (Input.GetKey(KeyCode.LeftArrow) && (Input.GetKey(KeyCode.LeftShift)))  
          || (Input.GetKey(KeyCode.RightArrow) && (Input.GetKey(KeyCode.LeftShift))) 
          || (Input.GetKey(KeyCode.UpArrow) && (Input.GetKey(KeyCode.LeftShift)))
          || (Input.GetKey(KeyCode.DownArrow) && (Input.GetKey(KeyCode.LeftShift)))
          )
        {
            
           playerAnimation.PlayerRunAnimation(false);
            playerAnimation.PlayerQuickAnimation(true);
            if (h > 0)
            myBody.velocity = new Vector2(moveForce_X*3, myBody.velocity.y);
            if (h < 0)
            myBody.velocity = new Vector2(-moveForce_X*3, myBody.velocity.y);
            if (v > 0)
             myBody.velocity = new Vector2(myBody.velocity.x, moveForce_Y*2);
            if (v < 0)
             myBody.velocity = new Vector2(myBody.velocity.x, -moveForce_Y*2);
              if(Input.GetKey(KeyCode.M))
            {
                playerAnimation.PlayerJumpAnimation(true);
                }
        }
        else if (myBody.velocity.x != 0 || myBody.velocity.y != 0)
        {
            playerAnimation.PlayerJumpAnimation(false);
             playerAnimation.PlayerRunAnimation(true);
           playerAnimation.PlayerQuickAnimation(false);
           if(Input.GetKey(KeyCode.M))
            {
                playerAnimation.PlayerJumpAnimation(true);
                }
        }
         else
        {
            playerAnimation.PlayerRunAnimation(false);
           playerAnimation.PlayerQuickAnimation(false);
            if(Input.GetKey(KeyCode.M) )
             {playerAnimation.PlayerJumpAnimation(true);
             }
          
        }

        Vector3 tempScale = transform.localScale;

        if (h > 0) {
            tempScale.x = -1f;

        } else if (h < 0) {
            tempScale.x = 1f;
        }//flip

        transform.localScale = tempScale;

    }
    void OnCollisionEnter(Collision col) {
        Debug.Log("test");
        playerAnimation.PlayerJumpAnimation(false);
        isGround = true;
    }

} // class










































