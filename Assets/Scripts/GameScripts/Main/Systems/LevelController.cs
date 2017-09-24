using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public static LevelController instance;
    public int level;

    private void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetLevel() {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("level" + level);
    }

    public void ExitApp() {
        Application.Quit();
    }

    public void GameOver() {
        GameObject.Find("SceneMusic").GetComponent<AudioSource>().Stop();
        GUIController.instance.gameOverUI.DoGameOver();
    }
   
}
