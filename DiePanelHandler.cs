using UnityEngine;
using UnityEngine.SceneManagement;

public class DiePanelHandler : MonoBehaviour
{
    public GameObject diePanel;           // Bảng "Die Panel"
    public MonoBehaviour cameraController; // Script điều khiển camera (ví dụ: FPSController)

    void Start()
    {
        // Ẩn bảng "Die Panel" và khóa chuột khi game bắt đầu
        if (diePanel != null)
        {
            diePanel.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked; // Khóa con trỏ chuột
        Cursor.visible = false; // Ẩn chuột
    }

    public void ShowDiePanel()
    {
        if (diePanel != null)
        {
            diePanel.SetActive(true);  // Hiển thị bảng "Die Panel"
            Time.timeScale = 0f;      // Dừng game

            // Hiển thị và mở khóa chuột
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;

            // Vô hiệu hóa script điều khiển camera
            if (cameraController != null)
            {
                cameraController.enabled = false; // Khóa camera
            }
        }
    }

    public void GoToMainMenu()
    {
        // Khôi phục trạng thái và chuyển về Main Menu
        Time.timeScale = 1f; // Tiếp tục game
        Cursor.lockState = CursorLockMode.None; // Đảm bảo chuột không bị khóa
        Cursor.visible = true; // Hiển thị chuột trên màn hình chính

        SceneManager.LoadScene("MainMenu"); // Thay tên Scene nếu cần
    }
}