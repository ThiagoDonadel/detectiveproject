using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeek : MonoBehaviour {


    public Spirit spirt;
    public GameObject mirror;

    // Use this for initialization
    void Start() {
        spirt.Move((Vector2)transform.position);
        StartCoroutine(ShakeMirror());
        //spirt.Move(new Vector2(7.5f,1.5f));
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        print("oi");
    }

    private IEnumerator ShakeMirror() {

        for(int i=0;i<4;i++) {
            mirror.transform.Rotate(0, 0, 10);
            yield return new WaitForSeconds(0.5f);
            mirror.transform.Rotate(0, 0, -20);
            yield return new WaitForSeconds(0.5f);
            mirror.transform.Rotate(0, 0, 10);
        }
        

        yield return null;
    }
}
