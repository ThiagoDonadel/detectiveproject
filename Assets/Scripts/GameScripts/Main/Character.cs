﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GameEnums;

public class Character : MonoBehaviour { 
    
    public float moveSpeed;
    public bool walking;
    public float speedModifier;

    private Animator walkAnimator;
    private IterativeObject targetObject;

    private Invetory inventory;

    private SpriteRenderer actionBallon;

    private void Awake() {
        inventory = new Invetory();
    }

    // Use this for initialization
    void Start () {
        walkAnimator = GetComponent<Animator>();
        actionBallon = GameObject.Find("ActionBallon").GetComponent<SpriteRenderer>();
        actionBallon.enabled = false;
    }

    private void Update() {
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

        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(movement == Vector2.zero) {
            walking = false;
            walkAnimator.SetBool("walking", walking);
        } else {
            walking = true;
            walkAnimator.SetBool("walking", walking);
            walkAnimator.SetFloat("x", movement.x);
            walkAnimator.SetFloat("y", movement.y);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + movement * Time.deltaTime * (moveSpeed + speedModifier));
        }      
    }

    private void OnCollisionEnter2D(Collision2D collision) {       
        if(collision.gameObject.tag == "Iteractive") {
            targetObject = collision.gameObject.GetComponent<IterativeObject>();
            actionBallon.enabled = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(targetObject != null) {
            if (targetObject.tag == "Iteractive") {
                actionBallon.enabled = false;
            }
            targetObject = null;            
        }
    }

    private void DoAction() {
        if(targetObject != null) {
            targetObject.Interact(transform.gameObject);
        }
    }
}
