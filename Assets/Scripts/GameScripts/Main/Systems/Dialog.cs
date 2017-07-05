using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    private Canvas canvas;
    public int maxSize;

    protected List<string> printList;

    private Text sentence;
    
    protected virtual void Awake() {
        canvas = transform.GetComponent<Canvas>();
        printList = new List<string>();
        sentence = transform.Find("DialogContainer").Find("Sentence").GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
        canvas.enabled = true;
        canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (canvas.enabled) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (printList.Count > 0) {
                    PrintText(printList[0]);
                    printList.RemoveAt(0);
                } else {
                    Hide();
                }
            }
        }
	}    

    public void ShowDialog(string text) {
        List<string> newLines = adjustTextLines(text);
        ShowDialog(newLines);
    }

    protected void ShowDialog(List<string> newLines) {
        foreach (string newLine in newLines) {
            if (canvas.enabled) {
                printList.Add(newLine);
            } else {
                PrintText(newLine);
                canvas.enabled = true;
            }
        }
    }

    protected List<string> adjustTextLines(string text) {
        List<string> newLines = new List<string>();

        while (text.Length > maxSize) {
            string newLine = text.Substring(0, maxSize);
            text = text.Substring(newLine.Length, text.Length - newLine.Length);
            newLines.Add(newLine);
        }

        newLines.Add(text);

        return newLines;
    }

    public void Hide() {
        canvas.enabled = false;
    }

    protected virtual void PrintText(string text) {
        sentence.text = text;
    }
}
