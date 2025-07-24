using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    public Transform orbitOrigin;
    public float baseRadius = 0.3f;
    public float itemScale = 0.1f;
    public int totalScore = 0;

    private List<GameObject> storedItems = new List<GameObject>();
    private Dictionary<GameObject, Vector3> originalScales = new Dictionary<GameObject, Vector3>();

    public InputActionReference removeItemAction;

    private void OnEnable()
    {
        if (removeItemAction != null)
            removeItemAction.action.Enable();
    }

    private void OnDisable()
    {
        if (removeItemAction != null)
            removeItemAction.action.Disable();
    }

    private void Update()
    {
        if (removeItemAction != null && removeItemAction.action.triggered && storedItems.Count > 0)
        {
            RemoveItem(storedItems[storedItems.Count - 1]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            GameObject item = other.gameObject;
            if (storedItems.Contains(item)) return;

            item.transform.SetParent(transform);

            var rb = item.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            AddItem(item);
        }
    }

    public void AddItem(GameObject item)
    {
        if (storedItems.Contains(item)) return;

        storedItems.Add(item);

        // บันทึกขนาดเดิมไว้ก่อนย่อ
        if (!originalScales.ContainsKey(item))
            originalScales[item] = item.transform.localScale;

        var grab = item.GetComponent<XRGrabInteractable>();
        if (grab) grab.enabled = false;

        item.SetActive(false);
        item.transform.SetParent(orbitOrigin);

        PointItem pointItem = item.GetComponent<PointItem>();
        if (pointItem != null)
        {
            totalScore += pointItem.scoreValue;
        }

        UpdateItemPositions();
    }

    public void RemoveItem(GameObject item)
    {
        if (!storedItems.Contains(item)) return;

        storedItems.Remove(item);

        item.transform.SetParent(null);
        item.transform.position = orbitOrigin.position + Random.onUnitSphere * baseRadius;

        // กลับไปใช้ขนาดเดิม
        if (originalScales.ContainsKey(item))
            item.transform.localScale = originalScales[item];
        else
            item.transform.localScale = Vector3.one;

        var rb = item.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        var grab = item.GetComponent<XRGrabInteractable>();
        if (grab) grab.enabled = true;

        item.SetActive(true);

        UpdateItemPositions();
    }

    private void UpdateItemPositions()
    {
        int count = storedItems.Count;
        float radius = baseRadius;

        for (int i = 0; i < count; i++)
        {
            GameObject item = storedItems[i];
            float angle = i * Mathf.PI * 2 / count;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            item.transform.position = orbitOrigin.position + pos;
            item.transform.localScale = Vector3.one * itemScale;

            item.SetActive(true);

            var grab = item.GetComponent<XRGrabInteractable>();
            if (grab) grab.enabled = true;
        }
    }
}
