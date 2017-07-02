﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GameEnums;

public class Character : MonoBehaviour {   

    public Direction currentDirection;
    public float moveSpeed;
    public bool walking;    

    private Animator walkAnimator;
    private IterativeObject targetObject;

    private Invetory inventory;

    private void Awake() {
        inventory = new Invetory();
    }

    // Use this for initialization
    void Start () {
        walkAnimator = GetComponent<Animator>();       
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.X)) {
            DoAction();
        } else {
            DoWalk();
        }
    }   

    public Invetory getInventory() {
        return inventory;
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

    private void OnCollisionEnter2D(Collision2D collision) {       
        if(collision.gameObject.tag == "Iteractive") {
            targetObject = collision.gameObject.GetComponent<IterativeObject>();
            LevelController.instance.messageController.ShowDialogBallon("...", transform.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(targetObject != null) {
            if (targetObject.tag == "Iteractive") {
                LevelController.instance.messageController.HideDialogBallon();
            }
            targetObject = null;            
        }
    }

    private void DoAction() {
        if(targetObject != null) {
            targetObject.Interact(transform.gameObject);
        }
    }

    private void UpdateAnimatorParams() {        
        walkAnimator.SetInteger("direction", ((int) currentDirection)+1);
        walkAnimator.SetBool("walking", walking);
    }

}