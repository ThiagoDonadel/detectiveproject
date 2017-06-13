using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour {

    public enum Direction { NONE, UP, DOWN, LEFT, RIGHT};

    public Direction currentDirection;
    public float moveSpeed;
    public bool walking;

    private Animator walkAnimator;

	// Use this for initialization
	void Start () {
        walkAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate() {

        DoWalk();

    }   

    private void DoWalk() {

        Direction movementDirection = Direction.NONE;

        float hMovement = Input.GetAxis("Horizontal");
        float vMovement = Input.GetAxis("Vertical");

        movementDirection = hMovement > 0 ? Direction.RIGHT : hMovement < 0 ? Direction.LEFT : movementDirection;
        movementDirection = vMovement > 0 ? Direction.UP : vMovement < 0 ? Direction.DOWN : movementDirection;

        if(movementDirection != Direction.NONE) {
            if (currentDirection != movementDirection) {
                currentDirection = movementDirection;
                walking = false;
                UpdateAnimatorParams();
                Input.ResetInputAxes();
            } else {                
                walking = true;
                UpdateAnimatorParams();
                Vector2 movement = new Vector2(hMovement * moveSpeed, vMovement * moveSpeed);
                GetComponent<Rigidbody2D>().velocity = movement;
            }
        } else {
            if(walking) {
                walking = false;
                UpdateAnimatorParams();
                GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);                
            }
        }

      
    }

    private void UpdateAnimatorParams() {        
        walkAnimator.SetInteger("direction", (int) currentDirection);
        walkAnimator.SetBool("walking", walking);
    }


}
