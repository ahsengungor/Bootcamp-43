using UnityEngine;

public class NPC_SinemaGise_StateMachine : MonoBehaviour
{
    public INPCState currentState;

    public Animator animator;

    [Header("Player Detection")]
    public Transform player;
    public float interactionRange = 3f;
    private bool isPlayerInRange = false;

    [SerializeField] public MouseBasedMovement playerController;
    [SerializeField] public DialogueData DialogueData;
    [SerializeField] public DialogueController DialogueController;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetState(new NPC_SinemaGise_Idle(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.Update();

        // Oyuncu belirli bir mesafeye girdiyse
        float distance = Vector3.Distance(player.position, transform.position);
        isPlayerInRange = distance <= interactionRange;

        if (isPlayerInRange)
        {
            //InteractionUI.Show("for Interact");
            if (Input.GetKeyDown(KeyCode.F))
            {
                InteractionUI.Hide();
                SetState(new NPC_SinemaGise_Interaction(this));
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
