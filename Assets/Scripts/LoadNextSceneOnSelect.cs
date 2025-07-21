using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class LoadNextSceneWithDelay : MonoBehaviour
{
    public string nextSceneName = "Level2"; 
    public float delaySeconds = 5f;         // �������� delay ��͹��Ŵ

    private bool isLoading = false;

    private void Awake()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelected);
        }
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        if (!isLoading)
        {
            isLoading = true;
            Debug.Log("�����˹�ǧ���� " + delaySeconds + " �Թҷ�...");
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delaySeconds);
        Debug.Log("����¹� Scene: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}
