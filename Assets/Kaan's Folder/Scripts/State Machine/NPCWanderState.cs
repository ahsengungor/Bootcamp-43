using UnityEngine;
using System.Collections;

public class NPCWanderState : INPCState
{
    private NPCStateMachine npc;
    private bool isWaiting = false;

    public NPCWanderState(NPCStateMachine npc)
    {
        this.npc = npc;
    }

    public void Enter()
    {
        MoveToRandomPoint();
        npc.animator.SetBool("isWalking", true);
    }

    public void Update()
    {
        if (!npc.agent.pathPending && npc.agent.remainingDistance <= npc.agent.stoppingDistance && !isWaiting)
        {
            npc.SetState(new NPCIdleState(npc));
        }
    }

    public void Exit() { }

    private void MoveToRandomPoint()
    {
        if (npc.wanderPoints.Length == 0) return;

        int index = Random.Range(0, npc.wanderPoints.Length);
        Vector3 destination = npc.wanderPoints[index].position;
        npc.agent.SetDestination(destination);
    }
}
