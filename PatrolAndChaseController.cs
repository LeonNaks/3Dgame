using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PatrolAndChaseController : MonoBehaviour
{
    public NavMeshAgent agent; // Thành phần NavMeshAgent
    public Transform[] patrolPoints; // Các điểm tuần tra
    public float patrolWaitTime = 2.0f; // Thời gian dừng tại điểm tuần tra
    public Transform player; // Vị trí của Player
    public string walkAnimation = "Walk"; // Hoạt ảnh tuần tra và đuổi theo
    public string idleAnimation = "Idle"; // Hoạt ảnh khi dừng lại
    public float detectionAngle = 60f; // Góc phát hiện (độ)
    public float detectionDistance = 10f; // Khoảng cách phát hiện

    private int currentPatrolIndex = 0; // Chỉ số điểm tuần tra hiện tại
    private bool isChasing = false; // Trạng thái đuổi theo Player
    private bool waiting = false; // Trạng thái chờ tại điểm tuần tra

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length > 0)
        {
            StartCoroutine(Patrol()); // Bắt đầu tuần tra
        }
    }

    void Update()
    {
        if (CanSeePlayer() && !isChasing)
        {
            isChasing = true; // Bật trạng thái đuổi theo nếu phát hiện Player
        }

        if (isChasing)
        {
            ChasePlayer(); // Đuổi theo Player khi đã phát hiện
        }
    }

    IEnumerator Patrol()
    {
        while (!isChasing)
        {
            if (!waiting && patrolPoints.Length > 0)
            {
                PlayAnimation(walkAnimation); // Phát hoạt ảnh đi bộ (tuần tra)
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);

                // Nếu nhân vật đến gần điểm tuần tra
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    waiting = true;
                    PlayAnimation(idleAnimation); // Dừng lại tại điểm tuần tra (Idle)
                    yield return new WaitForSeconds(patrolWaitTime); // Thời gian chờ
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Chuyển sang điểm tuần tra tiếp theo
                    waiting = false;
                }
            }
            yield return null; // Tiếp tục tuần tra
        }
    }

    void ChasePlayer()
    {
        agent.speed = 4.0f; // Tăng tốc độ khi đuổi theo
        PlayAnimation(walkAnimation); // Phát hoạt ảnh đi bộ (Walk) khi đuổi theo
        agent.SetDestination(player.position); // Di chuyển về phía Player
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Kiểm tra khoảng cách giữa nhân vật và Player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionDistance)
        {
            return false; // Ngoài phạm vi phát hiện
        }

        // Kiểm tra góc giữa hướng nhân vật và hướng đến Player
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle > detectionAngle / 2f)
        {
            return false; // Ngoài góc phát hiện
        }

        // Sử dụng Raycast để kiểm tra xem Player có bị chắn không
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true; // Phát hiện Player
            }
        }

        return false; // Không thấy Player
    }

    void PlayAnimation(string animationName)
    {
        Animation anim = GetComponent<Animation>();
        if (anim != null && anim.GetClip(animationName) != null && !anim.IsPlaying(animationName))
        {
            anim.Play(animationName);
        }
    }
}