using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Đối tượng người chơi
    public float speed = 3f; // Tốc độ di chuyển
    public float detectionRange = 10f; // Phạm vi phát hiện người chơi

    private Vector3 initialPosition; // Vị trí ban đầu của enemy

    private void Start()
    {
        initialPosition = transform.position; // Ghi nhớ vị trí ban đầu
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Di chuyển về phía người chơi
            ChasePlayer();
        }
        else
        {
            // Quay lại vị trí ban đầu nếu người chơi rời xa
            ReturnToStart();
        }
    }

    private void ChasePlayer()
    {
        // Xác định hướng di chuyển tới người chơi
        Vector3 direction = (player.position - transform.position).normalized;

        // Enemy di chuyển về phía người chơi
        transform.position += direction * speed * Time.deltaTime;

        // Enemy quay mặt về phía người chơi
        RotateTowards(direction);
    }

    private void ReturnToStart()
    {
        // Xác định hướng di chuyển về vị trí ban đầu
        Vector3 direction = (initialPosition - transform.position).normalized;

        // Enemy di chuyển về vị trí ban đầu
        transform.position += direction * speed * Time.deltaTime;

        // Enemy quay mặt về phía vị trí ban đầu
        RotateTowards(direction);
    }

    private void RotateTowards(Vector3 direction)
    {
        // Quay enemy về hướng mục tiêu
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}