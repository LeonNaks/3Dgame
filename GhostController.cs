using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform player; // Đối tượng Player
    public float detectionDistance = 5.0f; // Khoảng cách để kích hoạt con ma
    public float flickerDuration = 3.0f; // Thời gian chớp chớp tổng cộng
    public float flickerInterval = 0.2f; // Thời gian giữa mỗi lần chớp
    public AudioClip ghostSound; // Âm thanh của con ma
    private AudioSource audioSource; // Thành phần AudioSource
    private Renderer[] ghostParts; // Các phần của con ma
    private bool hasAppeared = false; // Kiểm tra trạng thái kích hoạt

    void Start()
    {
        // Lấy AudioSource và Renderer
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource không được gắn vào đối tượng " + gameObject.name);
            return;
        }

        ghostParts = GetComponentsInChildren<Renderer>();
        if (ghostParts.Length == 0)
        {
            Debug.LogError("Không tìm thấy Renderer trên đối tượng hoặc các phần con ma " + gameObject.name);
            return;
        }

        // Ẩn tất cả các phần ban đầu
        foreach (Renderer part in ghostParts)
        {
            part.enabled = false;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= detectionDistance && !hasAppeared)
        {
            StartCoroutine(ShowFlickerAndDisappear()); // Hiện chớp chớp và biến mất
        }
    }

    System.Collections.IEnumerator ShowFlickerAndDisappear()
    {
        hasAppeared = true; // Đảm bảo hiệu ứng chỉ xảy ra một lần
        float elapsedTime = 0f;

        // Bắt đầu phát nhạc khi con ma xuất hiện
        if (ghostSound != null)
        {
            audioSource.clip = ghostSound;
            audioSource.Play();
        }

        while (elapsedTime < flickerDuration)
        {
            foreach (Renderer part in ghostParts)
            {
                part.enabled = !part.enabled; // Đổi trạng thái ẩn/hiện
            }
            yield return new WaitForSeconds(flickerInterval); // Chờ trước khi đổi trạng thái
            elapsedTime += flickerInterval; // Cộng dồn thời gian đã qua
        }

        // Dừng nhạc và ẩn tất cả các phần của con ma sau khi chớp xong
        foreach (Renderer part in ghostParts)
        {
            part.enabled = false;
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // Tắt âm thanh
        }

        Debug.Log("Con ma đã biến mất cùng nhạc!");
    }
}