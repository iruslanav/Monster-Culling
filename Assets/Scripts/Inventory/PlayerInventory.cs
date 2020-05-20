using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{

    public List<InventoryItem> myInventory = new List<InventoryItem>();

    public void AddItem(InventoryItem thisItem)
    {
        if (thisItem)
        {
            if (myInventory.Contains(thisItem))
            {
                thisItem.numberHeld += 1;
            }
            else
            {
                myInventory.Add(thisItem);
                thisItem.numberHeld += 1;
            }
        }
    }
    public void RemoveItem(InventoryItem thisItem)
    {
        if (thisItem)
        {
            if (myInventory.Contains(thisItem))
            {
                thisItem.numberHeld -= 1;
            }
            else
            {
                myInventory.Remove(thisItem);
                thisItem.numberHeld -= 1;
            }
            if (thisItem.numberHeld < 0)
            {
                thisItem.numberHeld = 0;
            }
        }
    }

}
