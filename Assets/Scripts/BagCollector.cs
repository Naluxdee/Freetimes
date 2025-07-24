using UnityEngine;

public class BagCollector : MonoBehaviour
{
    public InventorySystem inventory;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[BagCollector] Trigger entered by: " + other.name);

        if (other.CompareTag("Item"))
        {
            Debug.Log("[BagCollector] Valid item detected: " + other.name);
            inventory.AddItem(other.gameObject);
        }
        else
        {
            Debug.Log("[BagCollector] Non-item entered: " + other.name);
        }
    }
}

