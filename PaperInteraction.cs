using UnityEngine;
using TMPro;

public class PaperInteraction : MonoBehaviour
{
    public GameObject paperObject; // Tờ giấy
    public TMP_Text paperText; // Văn bản hiển thị trên giấy
    private bool isPlayerInRange = false; // Kiểm tra người chơi trong phạm vi

    private void Start()
    {
        // Ẩn giấy và văn bản khi bắt đầu
        paperObject.SetActive(false);
        paperText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Nếu người chơi trong phạm vi và nhấn phím "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TogglePaper(); // Hiển thị hoặc ẩn giấy và văn bản
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Người chơi vào phạm vi
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Người chơi rời phạm vi

            // Ẩn giấy khi người chơi rời đi
            paperObject.SetActive(false);
            paperText.gameObject.SetActive(false);
        }
    }

    private void TogglePaper()
    {
        // Chuyển trạng thái hiển thị của giấy và văn bản
        bool isActive = paperObject.activeSelf; // Kiểm tra trạng thái hiện tại
        paperObject.SetActive(!isActive); // Hiển thị hoặc ẩn giấy
        paperText.gameObject.SetActive(!isActive); // Hiển thị hoặc ẩn văn bản
    }
}