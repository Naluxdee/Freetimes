using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue UI")]
    public Canvas dialogueCanvas;
    public TextMeshProUGUI dialogueText;
    public string[] messages;

    [Header("Input")]
    public InputActionReference startDialogueAction; // ��������顴������ʹ���

    [Header("Player Movement")]
    public Transform playerRig;         // XR Rig �ͧ������
    public Transform teleportTarget;    // �ش����ͧ�����

    [Header("Timer")]
    public float timerDuration = 300f;  // 5 �ҷ�

    private bool isTalking = false;
    private float timer;
    private bool timerStarted = false;

    private int messageIndex = 0;
    private bool dialogueStarted = false;

    void OnEnable()
    {
        startDialogueAction.action.Enable();
        startDialogueAction.action.performed += OnStartDialogue;
    }

    void OnDisable()
    {
        startDialogueAction.action.performed -= OnStartDialogue;
        startDialogueAction.action.Disable();
    }

    void Update()
    {
        if (timerStarted)
        {
            timer -= Time.deltaTime;
            dialogueText.text = $"���������: {Mathf.CeilToInt(timer)} �Թҷ�";

            if (timer <= 0)
            {
                timerStarted = false;
                dialogueText.text = "�������!";
            }
        }
    }

    void OnStartDialogue(InputAction.CallbackContext context)
    {
        if (!isTalking && !dialogueStarted)
        {
            // �������ʹ���
            dialogueStarted = true;
            dialogueCanvas.enabled = true;
            messageIndex = 0;
            dialogueText.text = messages[messageIndex];
        }
        else if (dialogueStarted && !isTalking)
        {
            // �Ѵ�㹺�ʹ���
            messageIndex++;

            if (messageIndex < messages.Length)
            {
                dialogueText.text = messages[messageIndex];
            }
            else
            {
                StartCoroutine(StartTimerCountdown());
            }
        }
    }

    IEnumerator StartTimerCountdown()
    {
        isTalking = true;
        dialogueText.text = "���ѧ�������áԨ...";
        yield return new WaitForSeconds(1f);

        // ���µ��˹觼�����
        if (playerRig != null && teleportTarget != null)
        {
            playerRig.position = teleportTarget.position;
            playerRig.rotation = teleportTarget.rotation;
        }

        yield return new WaitForSeconds(0.5f);

        // ������Ѻ����
        timer = timerDuration;
        timerStarted = true;
        isTalking = false;
    }
}
