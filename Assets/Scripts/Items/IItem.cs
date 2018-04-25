using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem {
    string name { get; set; }
    string baseValue { get; set; }
    string currentValue { get; set; }
    Sprite icon { get; set; }
    int? slotNum { get; set; }
    int quantity { get; set; }
    int maxQuantity { get; set; }
    bool stackable { get; set; }


}
