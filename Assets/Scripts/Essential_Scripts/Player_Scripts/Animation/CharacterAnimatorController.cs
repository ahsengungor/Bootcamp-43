using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimatorController : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 velocity = agent.velocity;
        float speed = velocity.magnitude;


        // Eğer velocity çok küçükse sıfırla (idle’a düşsün)
        if (speed < 0.1f)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
            return;
        }

        // Yerel yönü hesaba katmak için karakterin yönüne göre velocity'yi dönüştür
        Vector3 localVelocity = transform.InverseTransformDirection(velocity.normalized);

        // Blend Tree parametrelerini güncelle
        animator.SetFloat("MoveX", localVelocity.x);
        animator.SetFloat("MoveY", localVelocity.z);



    }
}
