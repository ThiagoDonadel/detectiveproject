using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientTable : IterativeObject {

    private int[] ingredients;
    private int next;
    private Canvas ingredientDialog;
    private Transform ingredientOptions;

    public Cauldron cauldron;

    void Start () {
        ingredientDialog = GetComponentInChildren<Canvas>();
        ingredientOptions = ingredientDialog.transform.Find("Panel").Find("IngredientOptions");
        ingredients = new int[3] { 0, 0, 0 };
        next = 0;
	}

    public override bool Interact(GameObject actor) {
        if(!ingredientDialog.enabled) {
            ingredientDialog.enabled = true;
        }
        return true;
    }

    public void CancelSelection() {

        for(int index=0;index<ingredients.Length;index++) {
            if (ingredients[index] != 0) {
                InvertSlots("ISel" + index, "IDisp" + ingredients[index]);
            }
        }

        ingredients = new int[3] { 0, 0, 0 };
        next = 0;
        ingredientDialog.enabled = false;
    }

    public void AddIngredients() {
        cauldron.AddIngredients(ingredients);
        CancelSelection();
    }

   public void SelectIngredient(int selected) {

        if (next < ingredients.Length) {

            InvertSlots("IDisp" + selected, "ISel" + next);            

            ingredients[next] = selected;

            if(next > 0) {
                ingredientOptions.Find("ISel" + (next-1)).GetComponentInChildren<Button>().interactable = false;
            }

            next++;
        }
   }    

    public void RemoveIngredient(int selected) {

        InvertSlots("ISel" + selected, "IDisp" + ingredients[selected]);

        if (selected > 0) {
            ingredientOptions.Find("ISel" + (selected - 1)).GetComponentInChildren<Button>().interactable = true;
        }

        ingredients[selected] = 0;
        next--;
    }

    private void InvertSlots(string originName, string targetName) {
        Button selectedItem = ingredientOptions.Find(originName).GetComponentInChildren<Button>();
        Button targetSlot = ingredientOptions.Find(targetName).GetComponentInChildren<Button>();

        Image selectedImage = selectedItem.transform.Find("Image").GetComponent<Image>();
        Image targetImage = targetSlot.transform.Find("Image").GetComponent<Image>();

        Sprite empty = targetImage.sprite;
        targetImage.sprite = selectedImage.sprite;
        targetSlot.interactable = true;

        selectedImage.sprite = empty;
        selectedItem.interactable = false;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        CancelSelection();
    }

}