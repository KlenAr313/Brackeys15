using UnityEngine;
using System.Collections;
using System.ComponentModel;
using UnityEngine.InputSystem;
using System.Linq.Expressions;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;


public class NPCInteractScript : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    [SerializeField] private float dialogueSpeed;
    [SerializeField] private string characterName;
    [SerializeField] private string[] sentences;
    [SerializeField] private bool triggering = false;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float frequency = 0.2f;
    [SerializeField] private float wob = 8f;
    private VisualElement dialogueRoot;
    private Label nameLabel;
    private bool skipRequested = false;
    private bool playerInRange = false;
    private bool dialogueActive = false;
    private int ind = 0;
    private Transform player;
    private bool doneWriting = true;
    private Coroutine writingCoroutine;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        document = gameObject.GetComponent<UIDocument>();
        
        if (document != null)
        {
            document.enabled = true;
            dialogueRoot = document.rootVisualElement.Q<VisualElement>("DialogueRoot");
            nameLabel = document.rootVisualElement.Q<Label>("NameLabel");
            nameLabel.style.display = DisplayStyle.None;
            nameLabel.text = characterName;
        }
    }

    void Update()
    {
        if (player == null || dialogueRoot == null) return;

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
            if (dialogueActive != true)
            {
                ShowDialogue();
            }
            NextSentence();
        }
        else if (!playerInRange && dialogueActive == true)
        {
            HideDialogue();
        }
    }

    void ShowDialogue()
    {
        dialogueActive = true;
        if (dialogueRoot != null || nameLabel != null)
            dialogueRoot.style.display = DisplayStyle.Flex;
            nameLabel.style.display = DisplayStyle.Flex;
    }

    void HideDialogue()
    {
        dialogueActive = false;
        
        if (dialogueRoot != null || nameLabel != null)
            dialogueRoot.style.display = DisplayStyle.None;
            nameLabel.style.display = DisplayStyle.None;
    }

    void NextSentence()
    {
        if (doneWriting)
        {
            if (ind <= sentences.Length - 1)
            {
                dialogueRoot.Clear();
                writingCoroutine = StartCoroutine(WriteSentence());
            }
            else
            {
                ind = 0;
                HideDialogue();
                if (triggering)
                {
                    gameObject.GetComponent<EnemyBase>().Trigger();
                }
            }
        }
        else
        {
            skipRequested = true;
        }
    }

    IEnumerator WriteSentence()
    {
        doneWriting = false;
        skipRequested = false;
        string sentence = sentences[ind];

        for(int i = 0; i < sentence.Length; i++)
        {
            AddLetter(sentence[i], i);
            
            if (!skipRequested)
                yield return new WaitForSeconds(dialogueSpeed);
            else
                continue;
        }

        doneWriting = true;
        ind++;
    }

    void AddLetter(char c, int index)
    {
        Label letter = new Label(c.ToString());
        letter.AddToClassList("Dialogue");

        letter.style.marginRight = 1;

        dialogueRoot.Add(letter);
        StartCoroutine(Wobble(letter, index));
    }

    IEnumerator Wobble(Label letter, int offset)
    {
        float t = offset * frequency;

        while (letter != null)
        {
            t += Time.deltaTime;

            float y = Mathf.Sin(t * 6f) * wob;
            letter.style.translate = new Translate(0, y, 0);

            yield return null;
        }
    }
}
