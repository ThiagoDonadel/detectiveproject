using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeTimer : MonoBehaviour {

    public int time;
    public Oven oven;

    private float timeRemaining;
    private AudioSource dangerBip;

	// Use this for initialization
	void Start () {
        timeRemaining = time;
        dangerBip = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Character") {
            Character player = other.gameObject.GetComponent<Character>();
            player.speedModifier = -1.0f;
            GUIController.instance.conversationDialog.ShowDialog(player.charName, "Coof.. Coof... Não Consigo respirar... é melhor acabar com essa fumaça");
            StartCoroutine(Danger());
            StartCoroutine(Suffocate());
        }
    }

    private IEnumerator Danger() {
        float pauseTime = dangerBip.clip.length+1;
        while (timeRemaining > 0) {
            if (oven.burning) {
                dangerBip.Play();
                yield return new WaitForSeconds(pauseTime);
                timeRemaining -= pauseTime;
                if (timeRemaining < 5) {
                    pauseTime = 0.5f;
                } else if (timeRemaining < 10) {
                    pauseTime = 1;
                }
            } else {
                break;
            }
        }      
    }



    private IEnumerator Suffocate() {
        yield return new WaitForSeconds(time);
        if(oven.burning) {
            GameObject.Find("Character").GetComponent<Character>().Kill(Character.KillTYpe.SMOKE);
        } else{
            Destroy(transform.gameObject);
        }
    }

}
