using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneBook : IterativeObject {

    public Color32 runeColor;
    public Sprite runeSprite;
    public string runeName;


    public override bool Interact(GameObject actor) {
        GameObject bookCanvas = transform.parent.Find("BookCanvas").gameObject;
        Image runeImage = bookCanvas.transform.Find("BookBase").transform.Find("BookRune").GetComponent<Image>();
        runeImage.sprite = runeSprite;
        runeImage.color = runeColor;
        Text runeText = bookCanvas.transform.Find("BookBase").transform.Find("RuneName").GetComponent<Text>();
        runeText.text = runeName;
        bookCanvas.GetComponent<Canvas>().enabled = true;
        return true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        transform.parent.Find("BookCanvas").GetComponent<Canvas>().enabled = false;
    }
}