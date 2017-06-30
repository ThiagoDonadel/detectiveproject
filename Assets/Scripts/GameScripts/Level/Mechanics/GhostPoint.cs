using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEnums;

public class GhostPoint : MonoBehaviour {

    public Direction x;
    public Direction y;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
       if(other.gameObject.tag == "Ghost") {
            HouseGhost ghost = other.GetComponent<HouseGhost>();
            if(ghost.currentDirection == Direction.DOWN || ghost.currentDirection == Direction.UP) {
                ghost.Turn(y);
            }  else {
                ghost.Turn(x);
            }
        }
    }
}
