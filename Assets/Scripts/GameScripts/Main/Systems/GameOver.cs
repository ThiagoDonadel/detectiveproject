using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    private AudioSource audioPlayer;
    private Canvas gameOverCanvas;

	// Use this for initialization
	void Start () {
        audioPlayer = GetComponent<AudioSource>();
        gameOverCanvas = GetComponent<Canvas>();
	}

    public void RetryClick() {
        LevelController.instance.ResetLevel();
    }

    public void ExitClick() {
        LevelController.instance.ExitApp();
    }
	
	public IEnumerator DoGameOver() {       
        yield return new WaitForSeconds(0.5f);
        audioPlayer.Play();
        gameOverCanvas.enabled = true;
    }


}