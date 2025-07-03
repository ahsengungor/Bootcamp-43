using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    [Header("UI References")]
    public GameObject DialoguePanel; 
    public Image leftCharacterImage;
    public Image leftShadowImage;
    public Image rightCharacterImage;
    public Image rightShadowImage;
    public TextMeshProUGUI dialogueText;

    [Header("Settings")]
    public float typingSpeed = 0.05f;

    private string[] lines;
    private int currentLine = 0;
    private Coroutine typingCoroutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Sahnedeki fazlalýðý önle
        }
        else
        {
            Instance = this;
        }
    }
    
    void Start() => DialoguePanel.SetActive(false);

    public void LoadDialogue(DialogueData data)
    {
        DialoguePanel.SetActive(true);
        leftCharacterImage.sprite = data.leftCharacter;
        leftShadowImage.sprite = data.leftShadow;
        rightCharacterImage.sprite = data.rightCharacter;
        rightShadowImage.sprite = data.rightShadow;

        lines = data.dialogueLines;
        currentLine = 0;

        StartTypingCurrentLine();
    }

    void StartTypingCurrentLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine(lines[currentLine]));
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueText.text == lines[currentLine])
            {
                currentLine++;
                if (currentLine < lines.Length)
                {
                    StartTypingCurrentLine();
                }
                else
                {
                    // Dialogue bitti
                    gameObject.SetActive(false);
                }
            }
            else
            {
                // Typing bitmeden space'e basýlýrsa hemen tamamla
                StopCoroutine(typingCoroutine);
                dialogueText.text = lines[currentLine];
            }
        }
    }
}
