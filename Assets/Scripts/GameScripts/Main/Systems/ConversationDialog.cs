using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConversationDialog : Dialog {

    private Text nameField;

    protected override void Awake() {
        nameField = transform.Find("DialogContainer").Find("Name").GetComponent<Text>();
        base.Awake();
    }

    public void ShowDialog(string name, string text) {
        List<string> newLines = adjustTextLines(text);

        foreach(string line in newLines) {
            newLines[newLines.IndexOf(line)] = name + "@" + line;
        }

        ShowDialog(newLines);
    }

    protected override void PrintText(string text) {
        string[] line = text.Split('@');

        nameField.text = line[0];
        base.PrintText(line[1]);
    }
}