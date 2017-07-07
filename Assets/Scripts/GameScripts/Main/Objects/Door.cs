using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : IterativeObject {

    
    public bool locked;
    public bool open;

    private Animator doorAnimator;


    // Use this for initialization
    protected virtual void Start () {
        doorAnimator = GetComponent<Animator>();
    }	

    public override bool Interact(GameObject actor) {

       if(locked) {
            Unlock(actor);
        } else {
            Open();
        }

       

        return true;
    }

    protected virtual void Unlock(GameObject actor) {
        GUIController.instance.eventDialog.ShowDialog("A porta está trancada!!!");
    }    

    protected virtual void Open() {
        open = true;    
        doorAnimator.SetBool("open", open);
        GetComponent<AudioSource>().Play();
    }

}