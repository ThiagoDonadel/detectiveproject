using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cipher : MonoBehaviour {

    public int[] digits;
    public PasswordDoor door;

	// Use this for initialization
	void Start () {
		digits =  new int[]{ 1,1,1,1 };
	}


    public void DigitUp(int digitIndex) {
        AlterDigit(digitIndex, 1);
    }

    public void DigitDown(int digitIndex) {
        AlterDigit(digitIndex, -1);
    }

    private void AlterDigit(int digitIndex, int modifier) {
        digits[digitIndex] += modifier;
        transform.Find("Digit" + (digitIndex+1)).Find("Text").GetComponent<Text>().text = digits[digitIndex].ToString();
        StartCoroutine(CheckPassword());
    }

    private IEnumerator CheckPassword() {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        door.checkPassword(GetCode());
    }

    private string GetCode() {
        string code = "";
        for(int index = 0; index < digits.Length; index++) {
            code += digits[index];
        }

        return code;
    }
}