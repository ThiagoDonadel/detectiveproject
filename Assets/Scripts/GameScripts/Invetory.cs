using System.Collections;
using System.Collections.Generic;

public class Invetory {

    private List<GameItem> items;

    public Invetory() {
        items = new List<GameItem>();        
    }

    public void Put(GameItem newItem) {
        items.Add(newItem);
    }

    public bool Carrying(string itemCode) {
        return items.Exists(items => items.code == itemCode);
    }

    public bool Consume(string itemCode) {

        bool consumed = false;

        if (Carrying(itemCode)) {
            consumed =  items.Remove(items.Find(items => items.code == itemCode));
        }

        return consumed;

    }
}
