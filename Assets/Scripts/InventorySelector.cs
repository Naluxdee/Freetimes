using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySelector : MonoBehaviour
{
    public LayerMask inventoryItemLayer; // ตั้งเป็นเฉพาะไอเทมในกระเป๋า
    public float maxDistance = 2f;
    public InventorySystem inventorySystem;

    private GameObject currentTarget;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, inventoryItemLayer))
        {
            currentTarget = hit.collider.gameObject;
            // อาจใส่ Highlight ด้วย
        }
        else
        {
            currentTarget = null;
        }

        // ตัวอย่างปุ่ม — แนะนำใช้จาก Input Action Map
        if (Keyboard.current.eKey.wasPressedThisFrame && currentTarget != null)
        {
            inventorySystem.RemoveItem(currentTarget);
        }
    }
}
