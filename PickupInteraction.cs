using UnityEngine;
using TMPro;

public class PickupInteraction : MonoBehaviour
{
    public GameObject paperObject; // Tờ giấy
    public GameObject pickupPrompt; // Giao diện hiện chữ "Nhấn E để nhặt"
    private bool isPlayerInRange = false; // Kiểm tra người chơi trong phạm vi

    private void Start()
    {
        // Ẩn giao diện nhặt lúc đầu
        pickupPrompt.SetActive(false);
    }

    private void Update()
    {
        // Nếu người chơi trong phạm vi và nhấn phím "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUpPaper(); // Nhặt tờ giấy
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Người chơi vào phạm vi
            pickupPrompt.SetActive(true); // Hiển thị giao diện nhặt
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Người chơi rời phạm vi
            pickupPrompt.SetActive(false); // Ẩn giao diện nhặt
        }
    }

    private void PickUpPaper()
    {
        // Thực hiện chức năng nhặt tờ giấy
        Debug.Log("Bạn đã nhặt tờ giấy!"); // Thông báo trên console
    }
}