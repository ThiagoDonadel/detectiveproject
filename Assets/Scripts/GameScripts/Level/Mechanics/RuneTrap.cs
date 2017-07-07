using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneTrap : MonoBehaviour {

    public string name;
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
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision) {
        pushed = false;        
    }

  

    private IEnumerator Explode() {
        yield return new WaitForSeconds(0.5f);
        if (pushed) {
            print(name + " Explodiu");
        }
    }
}