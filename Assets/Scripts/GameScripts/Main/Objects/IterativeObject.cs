using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterativeObject : MonoBehaviour {

    public virtual bool Interact(GameObject actor) {
        if(actor.tag == "Character") {
            LevelController.instance.messageController.ShowDialogBox("Não há nada aqui");
        }        
        return true;
    }
	
}
