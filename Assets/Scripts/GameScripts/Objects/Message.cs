using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : IterativeObject {

    public string message;   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool Interact(GameObject actor) {
        LevelController.instance.messageController.ShowDialogBox(message);
        return true;
    }


}
