using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCPlayerInteractionState : INPCState
{
    private NPCStateMachine npc;

    public NPCPlayerInteractionState(NPCStateMachine npc)
    {
        this.npc = npc;
    }

    public void Enter()
    {
        npc.agent.isStopped = true;
        npc.animator.SetBool("isWalking", false);

        npc.DialogueController.LoadDialogue(npc.DialogueData);
       
    }

    public void Update() { }

    public void Exit() { }

    private void OnDialogueEnd()
    {
        // Diyalog bitti�inde sahne/puzzle ekran� a�
        SceneManager.LoadScene("PuzzleSceneName"); // veya additive olarak y�kleyebilirsin
    }
}
