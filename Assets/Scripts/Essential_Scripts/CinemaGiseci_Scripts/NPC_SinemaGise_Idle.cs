using UnityEngine;

public class NPC_SinemaGise_Idle : MonoBehaviour, INPCState
{
    private NPC_SinemaGise_StateMachine npc;
    public NPC_SinemaGise_Idle(NPC_SinemaGise_StateMachine npc)
    {
        this.npc = npc;
    }
    public void Enter()
    {
        npc.animator.SetTrigger("Sit");
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
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
