using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    public static GUIController instance;
    
    public Dialog eventDialog;
    public ConversationDialog conversationDialog;
    public GameOver gameOverUI;

    private void Awake() {
       
    }

    // Use this for initialization
    void Start () {
        instance = this;

        eventDialog = this.transform.Find("EventDialog").GetComponent<Dialog>();
        conversationDialog = this.transform.Find("ConversationDialog").GetComponent<ConversationDialog>();
        gameOverUI = this.transform.Find("GameOverUI").GetComponent<GameOver>();
    }
	
	// Update is called once per frame
	void Update () {
		
	} 
}
