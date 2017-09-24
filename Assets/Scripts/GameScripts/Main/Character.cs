using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GameEnums;

public class Character : MonoBehaviour {

    public enum KillTYpe { EXPLOSION, GHOST, SMOKE, FIRE, ICE, STONE };

    public string charName;
    public float moveSpeed;
    public bool walking;
    public float speedModifier;
    public List<AudioClip> sounds;
   

    private Animator walkAnimator;
    private AudioSource audioPlayer;
    private IterativeObject targetObject;
    private bool imune = true;

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
        actionBallon.enabled = false;
        string killMethod = "";
        switch (type) {
            case KillTYpe.EXPLOSION:
                killMethod = "Explode";
                break;
            case KillTYpe.GHOST:
                killMethod = "GhostHug";
                break;
            case KillTYpe.SMOKE:
                killMethod = "Suffocate";
                break;
            case KillTYpe.FIRE:
                killMethod = "CatchFire";
                break;
            case KillTYpe.ICE:
                killMethod = "Freeze";
                break;
            case KillTYpe.STONE:
                killMethod = "Petrify";
                break;
        }

        StartCoroutine(killMethod);
    }

    private IEnumerator CatchFire() {
        GetComponent<SpriteRenderer>().color = Color.red;
        GUIController.instance.conversationDialog.ShowDialog(charName, "Que calor....");
        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().color = Color.white;
        walkAnimator.Play("OnFire");
        yield return new WaitForSeconds(1);
        LevelController.instance.GameOver();
    }

    private IEnumerator Freeze() {
        GetComponent<SpriteRenderer>().color = Color.blue;
        GUIController.instance.conversationDialog.ShowDialog(charName, "Que frioo....");
        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().color = Color.white;
        walkAnimator.Play("Freeze");
        yield return new WaitForSeconds(0.5f);
        LevelController.instance.GameOver();
    }

    private IEnumerator Petrify() {
        GetComponent<SpriteRenderer>().color = new Color32(75,75,75,255);
        GUIController.instance.conversationDialog.ShowDialog(charName, "Não consigo me mover....");
        yield return new WaitForSeconds(2);       
        LevelController.instance.GameOver();
    }

    

    private IEnumerator Suffocate() {
        GUIController.instance.conversationDialog.ShowDialog(charName, "Coof.. Coof.. Não.. Coff.. Consigo .. Coff.. Resp..");
        yield return new WaitForSeconds(0.5f);
        LevelController.instance.GameOver();        
    }

    private IEnumerator Explode() {       
        walkAnimator.Play("Explode");
        audioPlayer.PlayOneShot(sounds[1]);
        yield return new WaitForSeconds(0.5f);
        LevelController.instance.GameOver();        
    }

    private IEnumerator GhostHug() {
        GetComponent<SpriteRenderer>().color = new Color32(153, 253, 253, 255);
        yield return new WaitForSeconds(0.5f);
        LevelController.instance.GameOver();       
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

    public IEnumerator StayImune(float time) {
        List<Color> colors = new List<Color>();
        colors.Add(new Color32(242, 215, 2,170));      
        colors.Add(Color.white);
        float currTime = 0;
        float changeTimer = 0.3f;

        int current = 0;
        while(currTime < time) {
            GetComponent<SpriteRenderer>().color = colors[current];
            current++;
            if(current >= colors.Count) {
                current = 0;
            }
            yield return new WaitForSeconds(changeTimer);
            currTime += changeTimer;
        }

        GetComponent<SpriteRenderer>().color = Color.white;
    }

    
}
