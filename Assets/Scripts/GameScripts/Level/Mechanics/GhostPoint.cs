using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEnums;

public class GhostPoint : MonoBehaviour {

    public Direction[] directions;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
       if(other.gameObject.tag == "Ghost") {
            HouseGhost ghost = other.GetComponent<HouseGhost>();
            ghost.Turn(directions[Random.Range(0, directions.Length)]);            
        }
    }
}
