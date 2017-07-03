using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeTimer : MonoBehaviour {

    public int time;
    public Oven oven;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Character") {
            Character player = other.gameObject.GetComponent<Character>();
            player.speedModifier = -1.5f;
            StartCoroutine(Suffocate());
        }
    }

    private IEnumerator Suffocate() {
        yield return new WaitForSeconds(time);
        if(oven.burning) {
            LevelController.instance.ResetLevel();
        } else{
            Destroy(transform.gameObject);
        }
    }

}
