using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDoor : Door {

	public void ReleaseLock() {
        locked = false;
        Open();
    }

}