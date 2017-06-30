using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEnums;

public class GhostSpawn : MonoBehaviour {

    public int ghostNumber;
    public GameObject baseGhost;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Character") {

            GhostPoint[] gPoints = transform.parent.GetComponentsInChildren<GhostPoint>();

            
            float spawnX = 0;
            float spawnY = 0;
            GhostPoint from = gPoints[Random.Range(0, gPoints.Length)];

            Direction spawnDirection = Random.Range(0, 2) == 0 ? from.x : from.y;
            Vector2 directionVec = new Vector2();

            float offset1 = 0.5f;            

            switch (spawnDirection) {
                case Direction.UP:
                    directionVec = Vector2.up;
                    offset *= -1;
                    break;
                case Direction.RIGHT:
                    directionVec = Vector2.right;
                    break;
                case Direction.LEFT:
                    directionVec = Vector2.left;
                    break;
                case Direction.DOWN:
                    directionVec = Vector2.down;
                    break;
            }


            RaycastHit2D[] rayHit = new RaycastHit2D[1];
            from.GetComponent<CircleCollider2D>().Raycast(directionVec, rayHit);

            GhostPoint to = rayHit[0].transform.GetComponent<GhostPoint>();

            if (from.transform.position.x == to.transform.position.x) {
                spawnDirection = from.x;
                spawnX = from.transform.position.x;
                spawnY = Random.Range(from.transform.position.y+0.5f, to.transform.position.y);
            } else {
                spawnDirection = from.y;
                spawnY = from.transform.position.y;
                spawnX = Random.Range(from.transform.position.x + 0.5f, to.transform.position.x);
            }

            GameObject ghost = (GameObject)Instantiate(baseGhost);
            HouseGhost hGhost = ghost.GetComponent<HouseGhost>();
            hGhost.Spawn(spawnDirection, spawnX, spawnY);
            print(from + " - " + to + " - " + spawnDirection);

        }
    }

}
