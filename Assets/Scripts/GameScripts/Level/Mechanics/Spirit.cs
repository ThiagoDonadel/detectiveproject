using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour {

    public float speed = 2;
    public bool disapear;
    public string sName;


    private bool moving;
    private Vector2 moveTarget;
    private Vector2 hidePosition;

    private void Awake() {
        hidePosition = (Vector2)transform.position;
        disapear = false;
        Hide();
    }

    // Use this for initialization
    void Start() {
        
    }
	
	// Update is called once per frame
	void Update () {       
		if(moving) {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 direction = (moveTarget - rb.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
           
            if(Vector2.Distance(moveTarget, rb.position) < 0.1f) {
                Hide();
            }
            
        }
	}

    private void Reveal() {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    private void Hide() {
        if (disapear) {
            Destroy(transform.gameObject);
        } else {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().MovePosition(hidePosition);
            moving = false;
        }
    }


    public void Move(Vector2 destination, Vector2 newHidePosition) {        
        moveTarget = destination;
        hidePosition = newHidePosition;
        moving = true;
        Reveal();        
    }

    public string GetLine(int type) {

        return null;
    } 

}
