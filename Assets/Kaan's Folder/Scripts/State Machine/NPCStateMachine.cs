using UnityEngine;
using UnityEngine.AI;

public class NPCStateMachine : MonoBehaviour
{
    public INPCState currentState;

    [Header("Components")]
    public NavMeshAgent agent;
    public Animator animator;
    public Transform[] wanderPoints;

    [Header("Player Detection")]
    public Transform player;
    public float interactionRange = 3f;
    private bool isPlayerInRange = false;

    [SerializeField] public DialogueData DialogueData;
    [SerializeField] public DialogueController DialogueController;


    [SerializeField] public InteractionUI InteractionUI;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SetState(new NPCWanderState(this));
    }

    void Update()
    {
        currentState?.Update();

        // Oyuncu belirli bir mesafeye girdiyse
        float distance = Vector3.Distance(player.position, transform.position);
        isPlayerInRange = distance <= interactionRange;

        if (isPlayerInRange)
        {
            InteractionUI.Show("for Interact");
            if (Input.GetKeyDown(KeyCode.F))
            {
                InteractionUI.Hide();
                SetState(new NPCPlayerInteractionState(this));
            }
        }
    }

    public void SetState(INPCState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
