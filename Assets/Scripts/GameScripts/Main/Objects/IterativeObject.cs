using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterativeObject : MonoBehaviour {

    public void Hide() {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void Show() {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    public virtual bool Interact(GameObject actor) {
        if(actor.tag == "Character") {
            GUIController.instance.eventDialog.ShowDialog("Não há nada aqui");
        }        
        return true;
    }
	
}
