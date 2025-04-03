using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SoundDetectionController : MonoBehaviour
{
    public NavMeshAgent agent; // Thành phần NavMeshAgent
    public Transform[] patrolPoints; // Các điểm tuần tra
    public float patrolWaitTime = 2.0f; // Thời gian chờ tại điểm tuần tra
    public float hearingRadius = 10.0f; // Bán kính phát hiện âm thanh
    public Transform player; // Vị trí của Player
    public string walkAnimation = "Walk"; // Hoạt ảnh di chuyển
    public string idleAnimation = "Idle"; // Hoạt ảnh chờ (Idle)

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
        if (isChasing)
        {
            ChasePlayer(); // Đuổi theo Player khi nghe âm thanh
        }
    }

    IEnumerator Patrol()
    {
        while (!isChasing)
        {
            if (!waiting && patrolPoints.Length > 0)
            {
                PlayAnimation(walkAnimation); // Phát hoạt ảnh đi bộ
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);

                // Nếu nhân vật đến gần điểm tuần tra
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    waiting = true;
                    PlayAnimation(idleAnimation); // Chờ tại điểm tuần tra
                    yield return new WaitForSeconds(patrolWaitTime); // Thời gian dừng chờ
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Chuyển sang điểm tiếp theo
                    waiting = false;
                }
            }
            yield return null; // Tiếp tục tuần tra
        }
    }

    void ChasePlayer()
    {
        agent.speed = 4.0f; // Tăng tốc độ khi đuổi theo
        PlayAnimation(walkAnimation); // Phát hoạt ảnh đi bộ (Walk)
        agent.SetDestination(player.position); // Di chuyển về phía Player
    }

    void OnTriggerEnter(Collider other)
    {
        // Khi Player phát ra âm thanh (có Tag là "Player"), bắt đầu đuổi theo
        if (other.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Khi Player rời khỏi vùng nghe âm thanh, quay lại tuần tra
        if (other.CompareTag("Player"))
        {
            isChasing = false;
            agent.speed = 2.0f; // Trở lại tốc độ tuần tra
            StartCoroutine(Patrol()); // Quay lại chế độ tuần tra
        }
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