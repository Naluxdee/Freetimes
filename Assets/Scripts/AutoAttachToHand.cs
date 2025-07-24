using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class AutoAttachToAnyHand : MonoBehaviour
{
    private void Start()
    {
        var grab = GetComponent<XRGrabInteractable>();
        if (grab == null)
        {
            grab = gameObject.AddComponent<XRGrabInteractable>();
        }

        // หาทุก XRBaseInteractor ที่อยู่ในฉาก
        XRBaseInteractor[] allInteractors = FindObjectsOfType<XRBaseInteractor>();

        // หาตัวแรกที่ว่างและไม่ถือของอยู่
        foreach (var interactor in allInteractors)
        {
            if (interactor.hasSelection) continue;

            var interactionManager = interactor.interactionManager;
            if (interactionManager == null) continue;

            var interactorInterface = interactor as IXRSelectInteractor;
            var interactableInterface = grab as IXRSelectInteractable;

            // สั่งให้หยิบ
            interactionManager.SelectEnter(interactorInterface, interactableInterface);

            break; // หยุดเมื่อเจอมือลูกหนึ่งที่ว่าง
        }

        Destroy(this); // ใช้เสร็จแล้วลบ script ออก
    }
}
