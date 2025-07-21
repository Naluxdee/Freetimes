using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverCubeIndicator : MonoBehaviour
{
    public GameObject hoverCube; // Cube ที่จะแสดงเมื่อ hover
    private XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnHoverEnter);
            interactable.hoverExited.AddListener(OnHoverExit);
        }

        if (hoverCube != null)
            hoverCube.SetActive(false);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (hoverCube != null)
            hoverCube.SetActive(true);
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        if (hoverCube != null)
            hoverCube.SetActive(false);
    }
}
