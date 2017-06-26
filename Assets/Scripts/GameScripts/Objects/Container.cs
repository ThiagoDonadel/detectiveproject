using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : IterativeObject {

    public List<GameItem> items;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool Interact(GameObject actor) {
        
        if(actor.tag == "Character") {
            if (items.Count > 0) {
                Character character = actor.GetComponent<Character>();
                foreach (GameItem item in items) {
                    character.getInventory().Put(item);
                }
                items.Clear();
            } else {
                LevelController.instance.messageController.ShowDialogBox("Não há nada aqui");
            }
        }

        return true;

    }
}
