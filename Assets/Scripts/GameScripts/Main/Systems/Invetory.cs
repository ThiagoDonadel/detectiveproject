using System.Collections;
using System.Collections.Generic;

public class Invetory {

    private List<GameItem> items;

    public Invetory() {
        items = new List<GameItem>();        
    }

    public void Put(GameItem newItem) {
        items.Add(newItem);
        GUIController.instance.eventDialog.ShowDialog("O item " + newItem.name + " foi adquirido");
    }

    public bool Carrying(string itemCode) {
        return items.Exists(items => items.code == itemCode);
    }

    public bool Consume(string itemCode) {

        bool consumed = false;

        if (Carrying(itemCode)) {
            GameItem item = items.Find(items => items.code == itemCode);
            GUIController.instance.eventDialog.ShowDialog("O item " + item.name +" foi utilizado");
            consumed = items.Remove(item);
        }

        return consumed;

    }
}
