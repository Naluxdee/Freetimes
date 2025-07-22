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
    public InputActionReference startDialogueAction; // ปุ่มที่ใช้กดข้ามบทสนทนา

    [Header("Player Movement")]
    public Transform playerRig;         // XR Rig ของผู้เล่น
    public Transform teleportTarget;    // จุดที่ต้องย้ายไป

    [Header("Timer")]
    public float timerDuration = 300f;  // 5 นาที

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
            dialogueText.text = $"เหลือเวลา: {Mathf.CeilToInt(timer)} วินาที";

            if (timer <= 0)
            {
                timerStarted = false;
                dialogueText.text = "หมดเวลา!";
            }
        }
    }

    void OnStartDialogue(InputAction.CallbackContext context)
    {
        if (!isTalking && !dialogueStarted)
        {
            // เริ่มบทสนทนา
            dialogueStarted = true;
            dialogueCanvas.enabled = true;
            messageIndex = 0;
            dialogueText.text = messages[messageIndex];
        }
        else if (dialogueStarted && !isTalking)
        {
            // ถัดไปในบทสนทนา
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
        dialogueText.text = "กำลังเริ่มภารกิจ...";
        yield return new WaitForSeconds(1f);

        // ย้ายตำแหน่งผู้เล่น
        if (playerRig != null && teleportTarget != null)
        {
            playerRig.position = teleportTarget.position;
            playerRig.rotation = teleportTarget.rotation;
        }

        yield return new WaitForSeconds(0.5f);

        // เริ่มจับเวลา
        timer = timerDuration;
        timerStarted = true;
        isTalking = false;
    }
}
