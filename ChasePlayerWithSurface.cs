using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerWithSurface : MonoBehaviour
{
    public Transform player; // Đối tượng Player
    public float detectionDistance = 10.0f; // Khoảng cách phát hiện người chơi
    private NavMeshAgent agent; // Thành phần NavMeshAgent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionDistance)
        {
            agent.SetDestination(player.position); // Đặt đích là vị trí người chơi
        }
    }
}