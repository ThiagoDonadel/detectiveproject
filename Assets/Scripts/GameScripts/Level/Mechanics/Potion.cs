using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : IterativeObject {

    public List<Sprite> potionSprites;

    public enum PotEffect { None, Immune, Stone, Fire, Freeze }
    private PotEffect effect;

    private void Start() {
        Hide();
    }

    public void FillPotion(PotEffect effect) {
        GetComponent<SpriteRenderer>().sprite = potionSprites[Random.Range(0, potionSprites.Count)];
        this.effect = effect;
        Show();
    }

    public override bool Interact(GameObject actor) {
        Drink(actor.GetComponent<Character>());
        return true;
    }

    public void Drink(Character character) {

        Hide();
        
       switch (effect) {
            case PotEffect.Immune:
                StartCoroutine(character.StayImune(45));
                break;
            case PotEffect.Fire:
                character.Kill(Character.KillTYpe.FIRE);
                break;
            case PotEffect.Freeze:
                character.Kill(Character.KillTYpe.ICE);
                break;
           case PotEffect.Stone:
                character.Kill(Character.KillTYpe.STONE);
                break;
        }
    }

}
