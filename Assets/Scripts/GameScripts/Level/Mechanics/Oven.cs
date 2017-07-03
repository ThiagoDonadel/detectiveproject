using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : IterativeObject {

    public bool burning;
    public Container recipe;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public override bool Interact(GameObject actor) {

        if(burning) {
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            ps.Stop();
            burning = false;
            SpriteRenderer[] flame = GetComponentsInChildren<SpriteRenderer>();
            for (int index = 0; index < flame.Length; index++) {
                flame[index].enabled = false;
            }
            GameObject.Find("Character").GetComponent<Character>().speedModifier = 0;
            recipe.Show();
        } else {

            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            
            burning = true;
            SpriteRenderer[] flame = GetComponentsInChildren<SpriteRenderer>();
            for (int index = 0; index < flame.Length; index++) {
                flame[index].enabled = true;
            }
            ps.Play();
        }

        return true;
    }
}
