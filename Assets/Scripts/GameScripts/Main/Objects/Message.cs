using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : IterativeObject {

    public string[] messages;   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool Interact(GameObject actor) {
        if(messages.Length > 0) {
            foreach(String message in messages) {
               GUIController.instance.eventDialog.ShowDialog(message.Replace("#N","\n"));
            }           
        }        
        return true;
    }


}
