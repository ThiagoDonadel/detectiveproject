using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour {

    public float speed = 2;
    public string[] lines;

    private bool moving;
    private Vector2 targetPos;

    private void Awake() {
        moving = false;
        ShowHide();
    }

    // Use this for initialization
    void Start() {
        
    }
	
	// Update is called once per frame
	void Update () {       
		if(moving) {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 direction = (targetPos - rb.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
           
            if(Vector2.Distance(targetPos, rb.position) < 0.1f) {                
                moving = false;
                ShowHide();
            }
            
        }
	}
   

    public void ShowHide() {
        GetComponent<SpriteRenderer>().enabled = moving;
        //GetComponent<Collider2D>().enabled = moving;
    }


    public void Move(Vector2 destination) {        
        targetPos = destination;
        moving = true;
        ShowHide();        
    }

}
