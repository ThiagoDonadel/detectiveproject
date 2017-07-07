using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : IterativeObject {

    public AudioClip[] notes;
    public bool playing = false;
    public EventDoor targetDoor;

    private new AudioSource audio;
    private Canvas pianoCanvas;
    private string[] song = { "9", "0", "2", "9", "0", "3", "2", "9", "0", "2", "0", "9" };
    private int correctNotes;
    private bool songPlayed = false;

    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        pianoCanvas = GetComponentInChildren<Canvas>();
        playing = false;
        pianoCanvas.enabled = false;
        songPlayed = false;
        correctNotes = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}    

    public void PlayNote(int index) {
        audio.PlayOneShot(notes[index]);
        if (!songPlayed) {
            if (song[correctNotes] == index.ToString()) {
                correctNotes++;
                if(correctNotes == song.Length) {
                    targetDoor.ReleaseLock();
                    songPlayed = true;
                }
            } else {
                correctNotes = 0;
            }
        }      
    }

    public override bool Interact(GameObject actor) {

        if(actor.tag == "Character") {
            playing = !playing;
            pianoCanvas.enabled = playing;
            correctNotes = 0;            
        }

        return true;
    }

    public void OnCollisionExit2D(Collision2D collision) {
        if(playing) {
            playing = false;
            pianoCanvas.enabled = false;
        }
    }

}
