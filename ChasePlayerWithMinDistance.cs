using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerWithMinDistance : MonoBehaviour
{
    public Transform player; // Đối tượng Player
    public float detectionDistance = 10.0f; // Khoảng cách phát hiện người chơi
    public float chaseSpeed = 5.0f; // Tốc độ khi đuổi theo người chơi
    public float stopDistance = 2.0f; // Khoảng cách tối thiểu khi dừng (không tới sát quá)

    private NavMeshAgent agent; // Thành phần NavMeshAgent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent chưa được gắn vào đối tượng " + gameObject.name);
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionDistance && distanceToPlayer > stopDistance)
        {
            MoveTowardsPlayer(); // Di chuyển gần tới Player
        }
        else
        {
            StopMovement(); // Dừng lại khi ở trong khoảng cách tối thiểu
        }
    }

    void MoveTowardsPlayer()
    {
        if (agent.isActiveAndEnabled)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 targetPosition = player.position - directionToPlayer * stopDistance; // Giữ khoảng cách
            agent.SetDestination(targetPosition); // Đặt điểm đến gần Player nhưng không sát
            agent.speed = chaseSpeed;
        }
    }

    void StopMovement()
    {
        if (agent.isActiveAndEnabled)
        {
            agent.SetDestination(transform.position); // Ngừng di chuyển
        }
    }
}