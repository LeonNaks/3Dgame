using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Vị trí của Player
    public float triggerDistance = 2.0f; // Khoảng cách để kích hoạt sự kiện
    public GameObject dieScreen; // Bảng thông báo Die

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= triggerDistance)
        {
            TriggerDieEvent(); // Kích hoạt sự kiện khi Enemy đến gần
        }
    }

    void TriggerDieEvent()
    {
        dieScreen.SetActive(true); // Hiển thị bảng Die
        Time.timeScale = 0; // Dừng trò chơi (tùy chọn)
    }
}