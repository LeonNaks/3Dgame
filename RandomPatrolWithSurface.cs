using UnityEngine;
using UnityEngine.AI;

public class RandomPatrolWithSurface : MonoBehaviour
{
    public Vector3 patrolAreaCenter; // Tâm khu vực tuần tra
    public Vector3 patrolAreaSize; // Kích thước khu vực tuần tra
    private NavMeshAgent agent; // Thành phần NavMeshAgent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToRandomPoint(); // Bắt đầu tuần tra
    }

    void Update()
    {
        // Nếu đến đích, tạo điểm đến mới
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToRandomPoint();
        }
    }

    void MoveToRandomPoint()
    {
        Vector3 randomPoint = patrolAreaCenter + new Vector3(
            Random.Range(-patrolAreaSize.x / 2, patrolAreaSize.x / 2),
            0,
            Random.Range(-patrolAreaSize.z / 2, patrolAreaSize.z / 2)
        );

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position); // Di chuyển tới điểm hợp lệ
        }
        else
        {
            Debug.LogWarning("Không tìm thấy điểm hợp lệ trên NavMesh Surface!");
        }
    }
}