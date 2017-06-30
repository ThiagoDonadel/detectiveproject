using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    private Canvas canvas;
    public int maxSize;
    private List<string> printList;

    private void Awake() {
        canvas = transform.GetComponent<Canvas>();
        printList = new List<string>();
    }

    // Use this for initialization
    void Start () {
        canvas.enabled = true;
        canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(printList.Count > 0) {
                PrintText(printList[0]);
                printList.RemoveAt(0);
            } else {
                Hide();
            }
        }
	}

    public void ShowDialog(string text) {
        if(canvas.enabled) {
            printList.Add(text);
        } else {
            PrintText(text);
        }
       
       canvas.enabled = true; 
    }

    public void Hide() {
        canvas.enabled = false;
    }

    private void PrintText(string text) {
        canvas.GetComponentInChildren<Text>().text = text;
    }
}
