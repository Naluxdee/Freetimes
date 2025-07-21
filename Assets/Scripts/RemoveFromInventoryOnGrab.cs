using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RemoveFromInventoryOnGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private bool hasRemoved = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (hasRemoved) return;

        InventorySystem inventory = FindObjectOfType<InventorySystem>();
        if (inventory != null)
        {
            inventory.RemoveItem(gameObject);
            hasRemoved = true;
        }
    }

}
