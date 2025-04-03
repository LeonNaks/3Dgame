using UnityEngine;

public class MultiAnimationController : MonoBehaviour
{
    public Animation anim; // Thành phần Animation
    public Transform player; // Đối tượng Player
    public float detectionDistance = 10.0f; // Khoảng cách phát hiện người chơi
    public float attackDistance = 3.0f; // Khoảng cách để tấn công
    public string walkingAnimation = "Walk"; // Tên hoạt ảnh đi bộ
    public string attackAnimation = "Attack1"; // Tên hoạt ảnh tấn công
    public AnimationClip idleClip; // Clip Idle được gán trực tiếp trong mã

    private bool hasAttacked = false; // Kiểm tra trạng thái đã tấn công

    void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animation>();
        }
        if (anim == null)
        {
            Debug.LogError("Không tìm thấy thành phần Animation trên đối tượng " + gameObject.name);
            return;
        }

        // Gán hoạt ảnh Idle trực tiếp vào Animation Component
        if (idleClip != null && anim.GetClip(idleClip.name) == null)
        {
            anim.AddClip(idleClip, idleClip.name); // Thêm clip Idle vào Animation Component
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (hasAttacked)
        {
            PlayAnimation(idleClip.name); // Sau khi tấn công, luôn về Idle
            return;
        }

        if (distanceToPlayer <= attackDistance && !hasAttacked)
        {
            StartAttack(); // Tấn công nếu trong phạm vi
        }
        else if (distanceToPlayer <= detectionDistance)
        {
            PlayAnimation(walkingAnimation); // Đi bộ nếu trong phạm vi phát hiện
            MoveTowardsPlayer();
        }
        else
        {
            PlayAnimation(idleClip.name); // Idle nếu ngoài phạm vi phát hiện
        }
    }

    void StartAttack()
    {
        hasAttacked = true; // Đánh dấu đã tấn công
        PlayAnimation(attackAnimation);

        // Sau khi tấn công, quay về trạng thái Idle
        Invoke(nameof(EndAttack), anim.GetClip(attackAnimation).length); // Gọi hàm EndAttack sau khi kết thúc hoạt ảnh tấn công
    }

    void EndAttack()
    {
        PlayAnimation(idleClip.name); // Quay về hoạt ảnh Idle sau khi tấn công xong
    }

    void PlayAnimation(string animationName)
    {
        if (anim != null && anim.GetClip(animationName) != null && !anim.IsPlaying(animationName))
        {
            anim.Play(animationName);
        }
        else
        {
            Debug.LogError($"Hoạt ảnh '{animationName}' không tồn tại hoặc chưa được gán!");
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float speed = 1.0f; // Tốc độ di chuyển
        transform.position += directionToPlayer * speed * Time.deltaTime; // Di chuyển về phía Player
    }
}