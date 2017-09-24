using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cauldron : IterativeObject {

    public Potion potion;

    private int[] ingredients;
    private int[] mixOrder;

    private bool preparingPotion;
    private Canvas mixingCanvas;
    private Transform mixingDialog;
    private int next;

    private int[] formulaIngredients = new int[3] { 5,1,6 };
    private int[] formulaMixOrder = new int[3] { 1, -1, -1 };

    // Use this for initialization
    void Start () {
        ingredients = new int[3] { 0, 0, 0 };
        mixOrder = new int[3] { 0, 0, 0 };
        next = 0;
        preparingPotion = false;
        mixingCanvas = transform.Find("MixPotionCanvas").GetComponent<Canvas>();
        mixingDialog = mixingCanvas.transform.Find("MixPotionDialog");
        mixingCanvas.enabled = false;
    }

    public override bool Interact(GameObject actor) {
        
        if(!preparingPotion) {
            if(CheckIngredients()) {
                PreparePotion();
            } else {
                GUIController.instance.eventDialog.ShowDialog("Parece que alguns ingredientes estão faltando!!");
            }
        } 

        return true;
    }

    public void PreparePotion() {
        if (!preparingPotion) {
            preparingPotion = true;
            mixingDialog.GetComponentInChildren<Text>().text = "Os ingredientes estão no caldeirão!! Agora é necessário mecher a poção.. Escolha um sentido para mecher";
            mixingCanvas.enabled = true;
        } else {
            if(next >= mixOrder.Length) {
                CheckPrepareResult();
            } else {             
                mixingDialog.GetComponentInChildren<Text>().text = next == mixOrder.Length - 1 ? "Vamos lá ... mecha uma ultima vez..." : "Continue a mecher... ";
                mixingCanvas.enabled = true;
            }          
        }
    }

    private void CheckPrepareResult() {
       
        Potion.PotEffect effect = Potion.PotEffect.None; 
        print(ingredients + " - " + formulaIngredients);       
       if(ingredients[0] == formulaIngredients[0] && ingredients[1] == formulaIngredients[1] && ingredients[2] == formulaIngredients[2]) {
            
            if (mixOrder[0] == formulaMixOrder[0] && mixOrder[1] == formulaMixOrder[1] && mixOrder[2] == formulaMixOrder[2]) {
                effect = Potion.PotEffect.Immune;
            } else {
                effect = PrepareRandomPotion(10, 10);
            }

        } else {
            effect = PrepareRandomPotion(20, 7);
        }

        if(effect != Potion.PotEffect.None) {
            potion.FillPotion(effect);
        }

    }   

   private Potion.PotEffect PrepareRandomPotion(int range, int creationRange) {

        Potion.PotEffect effect = Potion.PotEffect.None;

        int random = Random.Range(0,range);

        if(random < creationRange) {
            effect = (Potion.PotEffect)Random.Range(2, 5);
        }

        return effect;
    }  

    public void MixPotion(int side) {
        mixingCanvas.enabled = false;
        mixOrder[next] = side;
        next++;
        PreparePotion();
    }


    private bool CheckIngredients() {
        return ingredients[0] != 0 && ingredients[1] != 0 && ingredients[2] != 0;
    }

    public void AddIngredients(int[] newIngredients) {
        ingredients = newIngredients;
    }
}