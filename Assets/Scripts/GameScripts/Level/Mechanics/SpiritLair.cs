using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritLair : IterativeObject {

    public bool hidden;

	// Use this for initialization
	void Start () {		
	}
	
	// Update is called once per frame
	void Update () {		
	}

    public override bool Interact(GameObject actor) {
        HideAndSeek mech = transform.parent.GetComponentInChildren<HideAndSeek>();
        if(mech != null) {
            mech.checkFound(this);
        } else {
            return base.Interact(actor);
        }
        
        return true;
    }
}
