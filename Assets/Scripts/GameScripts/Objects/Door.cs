using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : IterativeObject {

    public enum LockType { NONE, KEY, PASSWORD };
    public bool locked;
    public bool open;
    public LockType lockType = LockType.NONE;
    public string unlockWith;

    private Animator doorAnimator;


    // Use this for initialization
    void Start () {
        doorAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool Interact(GameObject actor) {

        bool successful = false;

        if (actor.tag == "Character") {
            Character character = actor.GetComponent<Character>();
            if (!open) {
                if(locked) {
                    if(lockType == LockType.KEY) {
                         
                    }
                } else {
                    Open();
                }
            }            
        }  

        return successful;
    }

    private void Open() {      
        open = true;
        doorAnimator.SetBool("open", open);
    }

}
