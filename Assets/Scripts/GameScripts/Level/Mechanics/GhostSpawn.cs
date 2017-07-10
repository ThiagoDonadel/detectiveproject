using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEnums;

public class GhostSpawn : MonoBehaviour {

    public int ghostNumber;
    public GameObject baseGhost;

    private bool started = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
      
        if(other.gameObject.tag == "Character" && !started) {

            GhostPoint[] gPoints = transform.parent.GetComponentsInChildren<GhostPoint>();

            for (int currGhosts = 1; currGhosts <= ghostNumber; currGhosts++) {
                float spawnX = 0;
                float spawnY = 0;
                GhostPoint from = gPoints[Random.Range(0, gPoints.Length)];

                Direction spawnDirection = from.directions[Random.Range(0, from.directions.Length*2)/2];
                Vector2 directionVec = new Vector2();

                float offset = 0.5f;

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

                CircleCollider2D fromCollider = from.GetComponent<CircleCollider2D>();
                RaycastHit2D[] rayHit = new RaycastHit2D[1];
                fromCollider.Raycast(directionVec, rayHit, 10, LayerMask.GetMask("SceneTop"));
                CircleCollider2D toCollider = (CircleCollider2D)rayHit[0].collider;

                Vector2 fromPoint = fromCollider.transform.TransformPoint(fromCollider.offset);
                Vector2 toPoint = toCollider.transform.TransformPoint(toCollider.offset);

                if (fromPoint.x == toPoint.x) {                  
                    spawnX = fromPoint.x;
                    spawnY = Random.Range(fromPoint.y, toPoint.y);
                } else {                   
                    spawnY = fromPoint.y;
                    spawnX = Random.Range(fromPoint.x, toPoint.x);
                }

                GameObject ghost = (GameObject)Instantiate(baseGhost, transform.parent);
                HouseGhost hGhost = ghost.GetComponent<HouseGhost>();
                hGhost.Spawn(spawnDirection, spawnX, spawnY);
            }

            GetComponent<AudioSource>().Play();
            Character character = other.GetComponent<Character>();
            GUIController.instance.conversationDialog.ShowDialog(character.charName, "OOHH... Esses fantasmas parecem perigosos melhor eu não deixar eles se aproximarem!!");
            started = true;
            StartCoroutine(AutoDestroy());
        }
    }

    private IEnumerator AutoDestroy() {
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Destroy(transform.gameObject);
    }

}
