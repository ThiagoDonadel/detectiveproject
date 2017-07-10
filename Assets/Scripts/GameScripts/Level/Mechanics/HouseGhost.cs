using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEnums;

public class HouseGhost : MonoBehaviour {

    public float speed;
    public Sprite[] spritesMap;
    public Direction currentDirection = Direction.RIGHT;    

    public void Spawn(Direction startDirection, float x, float y) {
        currentDirection = startDirection;
        transform.position = new Vector3(x, y, 1);
        ChangeSprite();
        StartMoving();
    }

    public void Turn(Direction newDirection) {
        currentDirection = newDirection;
        ChangeSprite();
        StartMoving();
    }

    private void ChangeSprite() {
        SpriteRenderer sr = transform.GetComponent<SpriteRenderer>();
        sr.sprite = spritesMap[(int)currentDirection];
    }

    private void StartMoving() {
        Vector2 direction = new Vector2();
        switch (currentDirection) {
            case Direction.UP:
                direction = Vector2.up;
                break;
            case Direction.RIGHT:
                direction = Vector2.right;
                break;
            case Direction.LEFT:
                direction = Vector2.left;
                break;
            case Direction.DOWN:
                direction = Vector2.down;
                break;
        }

        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Character") {
            other.GetComponent<Character>().Kill(Character.KillTYpe.GHOST);
            Destroy(GameObject.Find("GhostPoints"));
        }
    }
   
}
