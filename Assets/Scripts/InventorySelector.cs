using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySelector : MonoBehaviour
{
    public LayerMask inventoryItemLayer; // �����੾������㹡�����
    public float maxDistance = 2f;
    public InventorySystem inventorySystem;

    private GameObject currentTarget;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, inventoryItemLayer))
        {
            currentTarget = hit.collider.gameObject;
            // �Ҩ��� Highlight ����
        }
        else
        {
            currentTarget = null;
        }

        // ������ҧ���� � �й���ҡ Input Action Map
        if (Keyboard.current.eKey.wasPressedThisFrame && currentTarget != null)
        {
            inventorySystem.RemoveItem(currentTarget);
        }
    }
}
