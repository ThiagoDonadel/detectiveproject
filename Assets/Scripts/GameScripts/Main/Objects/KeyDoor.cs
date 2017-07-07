using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Door {

    public string key;

    protected override void Unlock(GameObject actor) {

        if (actor.tag == "Character") {
            Character character = actor.GetComponent<Character>();

            if (character.getInventory().Consume(key)) {
                locked = false;
                Open();
            } else {
                base.Unlock(actor);
            }

        }
    }

}