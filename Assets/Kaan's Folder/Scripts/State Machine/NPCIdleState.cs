using UnityEngine;
using System.Collections;

public class NPCIdleState : INPCState
{
    private NPCStateMachine npc;
    private float waitTime;

    public NPCIdleState(NPCStateMachine npc)
    {
        this.npc = npc;
    }

    public void Enter()
    {
        npc.animator.SetBool("isWalking", false);
        waitTime = Random.Range(3f, 5f);
        npc.StartCoroutine(IdleWait());
    }

    public void Update() { }

    public void Exit()
    {
        npc.StopAllCoroutines();
    }

    private IEnumerator IdleWait()
    {
        yield return new WaitForSeconds(waitTime);
        npc.SetState(new NPCWanderState(npc));
    }
}
