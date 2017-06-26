using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour {
    
    private Dialog boxDialog;
    private Dialog ballonDialog;

    private void Awake() {
        boxDialog = this.transform.Find("DialogBox").GetComponent<Dialog>();
        ballonDialog = this.transform.Find("DialogBallon").GetComponent<Dialog>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ShowDialogBox(string text) {        
        boxDialog.ShowDialog(text);       
    }

    public void HideDialogBox() {
        boxDialog.Hide();
    }

    public void ShowDialogBallon(string text, GameObject ballonTarget) {
        Vector3 targetPosition = Camera.main.WorldToScreenPoint(ballonTarget.transform.position);
        ballonDialog.ShowDialog(text);
        targetPosition.y += 25;
        ballonDialog.GetComponentInChildren<Image>().transform.position = targetPosition;
    }

    public void HideDialogBallon() {
        ballonDialog.Hide();
    }






}
