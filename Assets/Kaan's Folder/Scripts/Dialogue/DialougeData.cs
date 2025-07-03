using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
public class DialogueData : ScriptableObject
{
    public Sprite leftCharacter;
    public Sprite leftShadow;

    public Sprite rightCharacter;
    public Sprite rightShadow;

    [TextArea] public string[] dialogueLines;
}
