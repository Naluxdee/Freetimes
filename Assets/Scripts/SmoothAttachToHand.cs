using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

[RequireComponent(typeof(XRGrabInteractable))]
public class SmoothAttachToHand : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Transform attachTarget; // attachTransform ของมือ

    public float moveDuration = 0.3f;

    private Coroutine moveCoroutine;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(StartMoveToHand);
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(StartMoveToHand);
    }

    private void StartMoveToHand(SelectEnterEventArgs args)
    {
        Debug.Log("เริ่มลอยเข้ามือ");

        XRBaseInteractor baseInteractor = args.interactorObject as XRBaseInteractor;
        if (baseInteractor == null)
        {
            Debug.LogWarning("Interactor ไม่ใช่ XRBaseInteractor");
            return;
        }

        attachTarget = baseInteractor.attachTransform;

        if (attachTarget == null)
        {
            Debug.LogWarning("attachTransform ของ interactor ยังไม่ถูกตั้งค่า");
            return;
        }

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveToHand());
    }

    private IEnumerator MoveToHand()
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        Vector3 endPos = attachTarget.position;
        Quaternion endRot = attachTarget.rotation;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);

            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);

            yield return null;
        }

        transform.position = endPos;
        transform.rotation = endRot;

        moveCoroutine = null;
    }
}
