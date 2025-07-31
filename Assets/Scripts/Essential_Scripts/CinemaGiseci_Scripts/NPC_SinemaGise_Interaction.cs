using UnityEngine;

public class NPC_SinemaGise_Interaction : MonoBehaviour, INPCState
{
    private NPC_SinemaGise_StateMachine npc;

    public NPC_SinemaGise_Interaction(NPC_SinemaGise_StateMachine npc)
    {
        this.npc = npc;
    }

    public void Enter()
    {
        npc.animator.SetTrigger("Talk");

        npc.DialogueController.LoadDialogue(npc.DialogueData);
        npc.playerController.canMove = false;
    }

    public void Exit()
    {
        npc.playerController.canMove = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void INPCState.Update()
    {
        throw new System.NotImplementedException();
    }
}
