using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 0.05f;
    public float lineDelay = 1f; // Bir satır bittikten sonra bekleme süresi

    private int index = 0;

    void Start()
    {
        dialogueText.text = "";
        StartCoroutine(RunDialogue());
    }

    IEnumerator RunDialogue()
    {
        while (index < lines.Length)
        {
            yield return StartCoroutine(TypeLine(lines[index]));
            yield return new WaitForSeconds(lineDelay);
            index++;
        }

        dialogueText.text = "";
        gameObject.SetActive(false); // Diyalog kutusu bittiğinde kapanır
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}