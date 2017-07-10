using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneTrap : MonoBehaviour {
   
    public bool safe;

    private bool pushed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        pushed = true;
        if (!safe) {
            StartCoroutine(Explode());
        } else {
            GetComponent<SpriteRenderer>().color = new Color32(253, 74, 74, 255); 
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision) {
        pushed = false;        
    }

    private IEnumerator Explode() {
        yield return new WaitForSeconds(0.4f);
        if (pushed) {
            GameObject.Find("Character").GetComponent<Character>().Kill(Character.KillTYpe.EXPLOSION);
        }
    }
}