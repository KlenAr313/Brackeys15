using UnityEngine;
using TMPro;
using System.Collections;
using System.ComponentModel;
using UnityEngine.InputSystem;
using System.Linq.Expressions;
using UnityEngine.InputSystem.Controls;
using System.Reflection;

public class NPCInteractScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCDialogue;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private float dialogueSpeed;
    [SerializeField] private string[] sentences;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    
    private bool playerInRange = false;
    private bool dialogueActive = false;
    private int ind = 0;
    private Transform player;
    private bool doneWriting = true;
    private Coroutine writingCoroutine;

    void Start()
    {
        // MAKE PLAYER BEFORE
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (dialogueCanvas != null)
            dialogueCanvas.enabled = false;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        playerInRange = distanceToPlayer <= interactionRange;

        bool pressed = false;

        if (Keyboard.current != null)
        {
            Key parsedKey;
            if (System.Enum.TryParse<Key>(interactionKey.ToString(), out parsedKey))
            {
                KeyControl keyControl = Keyboard.current[parsedKey];
                if (keyControl != null && keyControl.wasPressedThisFrame)
                    pressed = true;
            }
        }


        if (playerInRange && pressed)
        {
            ShowDialogue();
            NextSentence();
        }
        else if (!playerInRange && dialogueActive == true)
        {
            HideDialogue();
        }
    }

    private void ShowDialogue()
    {
        dialogueActive = true;
        if (dialogueCanvas != null)
        {
            dialogueCanvas.enabled = true;
            
        }
    }

    private void HideDialogue()
    {
        dialogueActive = false;
        if (dialogueCanvas != null)
            dialogueCanvas.enabled = false;
        
        if (NPCDialogue != null)
            NPCDialogue.text = "";
    }

    void NextSentence()
    {
        if (doneWriting)
        {
            if (ind <= sentences.Length - 1)
            {
                NPCDialogue.text = "";
                writingCoroutine = StartCoroutine(WriteSentence());
            }
            else
            {
                ind = 0;
                HideDialogue();
            }
        }
        else
        {
            StopCoroutine(writingCoroutine);
            NPCDialogue.text = sentences[ind];
            doneWriting = true;
            ind++;
        }
    }

    IEnumerator WriteSentence()
    {
        doneWriting = false;
        foreach(char character in sentences[ind].ToCharArray())
        {
            NPCDialogue.text += character;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        doneWriting = true;
        ind++;
    }
}
