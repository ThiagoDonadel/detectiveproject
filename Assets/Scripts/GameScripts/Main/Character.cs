using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GameEnums;

public class Character : MonoBehaviour {

    public enum KillTYpe { EXPLOSION, GHOST, SMOKE };

    public string charName;
    public float moveSpeed;
    public bool walking;
    public float speedModifier;
    public List<AudioClip> sounds;    

    private Animator walkAnimator;
    private AudioSource audioPlayer;
    private IterativeObject targetObject;

    private Invetory inventory;

    private SpriteRenderer actionBallon;

    private void Awake() {
        inventory = new Invetory();
    }

    // Use this for initialization
    void Start () {
        walkAnimator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        actionBallon = GameObject.Find("ActionBallon").GetComponent<SpriteRenderer>();
        actionBallon.enabled = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
            DoAction();
        } else {
            DoWalk();
        }
    }   

    public Invetory getInventory() {
        return inventory;
    }

    private void DoWalk() {

        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(movement == Vector2.zero) {
            walking = false;
            walkAnimator.SetBool("walking", walking);
        } else {
            walking = true;
            walkAnimator.SetBool("walking", walking);
            walkAnimator.SetFloat("x", movement.x);
            walkAnimator.SetFloat("y", movement.y);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + movement * Time.deltaTime * (moveSpeed + speedModifier));
        }      
    }

    public void NewItem() {
        audioPlayer.PlayOneShot(sounds[0]);
    }


    public void Kill(KillTYpe type) {
        moveSpeed = 0;
        switch (type) {
            case KillTYpe.EXPLOSION:
                Explode();
                break;
            case KillTYpe.GHOST:
                GhostHug();
                break;
            case KillTYpe.SMOKE:
                break;
        }       
        LevelController.instance.GameOver();
    }

    private void Suffocate() {
        GUIController.instance.conversationDialog.ShowDialog(charName, "Coof.. Coof.. Não.. Coff.. Consigo .. Coff.. Resp..");
    }

    private void Explode() {       
        walkAnimator.Play("Explode");
        audioPlayer.PlayOneShot(sounds[1]);
    }

    private void GhostHug() {
        GetComponent<SpriteRenderer>().color = new Color32(153, 253, 253, 255);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Iteractive") {
            TargetObject(collision.gameObject, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Iteractive") {
            TargetObject(collision.gameObject, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Iteractive") {
            TargetObject(other.gameObject, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Iteractive") {
            TargetObject(other.gameObject, false);
        }
    }

    private void TargetObject(GameObject obj, bool target) {
        
            if(target) {
                targetObject = obj.GetComponent<IterativeObject>();
                actionBallon.enabled = true;
            } else {
                actionBallon.enabled = false;
                targetObject = null;
            }
    }

    private void DoAction() {
        if(targetObject != null) {
            targetObject.Interact(transform.gameObject);
        }
    }
}
