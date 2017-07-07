using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordDoor : Door {

    public string password;

    private Canvas passCanvas;

	// Use this for initialization
	protected override void Start () {
        passCanvas = GetComponentInChildren<Canvas>();
        base.Start();
	}

    protected override void Unlock(GameObject actor) {
        passCanvas.enabled = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (passCanvas != null) {
            passCanvas.enabled = false;
        }
    }

    protected override void Open() {
        Destroy(passCanvas.gameObject);
        base.Open();
    }

    public void checkPassword(string tryPass) {
        if(password == tryPass) {
            locked = false;
            Open();
        }
    }

}