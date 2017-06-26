using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : IterativeObject {

    public enum LockType { NONE, KEY, PASSWORD };
    public bool locked;
    public bool closed;
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
            if (closed) {
                if (locked) {
                    if (lockType == LockType.KEY) {
                        if(character.getInventory().Consume(unlockWith)) {
                            locked = false;
                            Open();                            
                        } else {
                            LevelController.instance.messageController.ShowDialogBox("A porta está trancada!!!");
                        }                        
                    }
                } else {
                    Open();
                }
            }
           
        }  

        return successful;
    }

    private void Open() {      
        closed = false;     
        doorAnimator.SetBool("open", !closed);
    }

}
