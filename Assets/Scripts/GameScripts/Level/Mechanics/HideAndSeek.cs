using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeek : MonoBehaviour {


    public Spirit spirt;
    public SpiritLair lair;

    private int found;
    private int attempts;
    private SpiritLair lastSearch;
    private bool started;

    // Use this for initialization
    void Start() {
        found = 0;
        attempts = 0;
        started = false;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Character" && !started) {
            StartCoroutine(ShakeObject(lair, 4, 2));
            GUIController.instance.conversationDialog.ShowDialog("?????", "INVASOR!! VÁ EMBORA ... VOCÊ NUNCA TERA AQUILO QUE ME PERTENCE");
            started = true;
        } else if(other.gameObject == spirt.gameObject) {
            print("espirito passou");
        }
    }

    private IEnumerator ShakeObject(SpiritLair target, int times, float waitToStart ) {

        yield return new WaitForSeconds(waitToStart);
        if (target.hidden) {
            for (int i = 0; i < times; i++) {
                target.transform.Rotate(0, 0, 10);
                yield return new WaitForSeconds(0.3f);
                target.transform.Rotate(0, 0, -20);
                yield return new WaitForSeconds(0.3f);
                target.transform.Rotate(0, 0, 10);
            }
        }

        yield return null;
    }

    public void checkFound(SpiritLair searching) {
        string speech;
        if (searching.hidden) {
            found++;
            attempts = 0;
            lair.hidden = false;
            speech = "Você nunca terá aquilo que me pertence";
        } else {
            speech = "Você nunca irá me achar... HA HA HA HA";
        }      

        if (found == 4) {
            Vector2 lastPoint = (Vector2)transform.position + Vector2.down;
            spirt.Move(lastPoint, lastPoint);
            speech = "Você ganhou...";
        } else {
          
            SpiritLair[] lairs = transform.parent.GetComponentsInChildren<SpiritLair>();
            SpiritLair newLair;
            do {
                newLair = lairs[Random.Range(0, lairs.Length)];               
            } while (newLair.gameObject == searching.gameObject || newLair.gameObject == lair.gameObject);
           
            Vector2 hideLocation = newLair.transform.position;
            newLair.hidden = true;
            spirt.Move(new Vector2(7.5f,1.5f), hideLocation);
            lair = newLair;

            if(searching != lastSearch) attempts++;            

            if (attempts % 4 == 0) {
                StartCoroutine(ShakeObject(lair, 2, 3));
            }

            lastSearch = searching;

        }

        GUIController.instance.conversationDialog.ShowDialog("Espirito Ancião", speech);
    }
}