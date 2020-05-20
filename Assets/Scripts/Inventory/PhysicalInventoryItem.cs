
using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.isTrigger)
        {
            AddItemTOInventory();
            Destroy(this.gameObject);
        }
        
    }
    void AddItemTOInventory()
    {
        playerInventory.AddItem(thisItem);
    }
}
