using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : IterativeObject {

    private int[] ingredients;
    private int[] mixOrder;

    private bool preparingPotion;

	// Use this for initialization
	void Start () {
        ingredients = new int[3] { 0, 0, 0 };
        mixOrder = new int[3] { -1, -1, -1 };
        preparingPotion = false;
    }

    public override bool Interact(GameObject actor) {
        
        if(!preparingPotion) {
            if(CheckIngredients()) {
                MixPotion();
            } else {
                GUIController.instance.eventDialog.ShowDialog("Parece que alguns ingredientes estão faltando!!");
            }
        } 

        return true;
    }

    public void PreparePotion() {
        preparingPotion = true;
    }

    public void MixPotion() {
        
    }


    private bool CheckIngredients() {
        return ingredients[0] != 0 && ingredients[1] != 0 && ingredients[2] != 0;
    }

    public void AddIngredients(int[] newIngredients) {
        ingredients = newIngredients;
    }
}