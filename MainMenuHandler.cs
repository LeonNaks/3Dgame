using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject diePanel; // Bảng "Die Panel"

    void Start()
    {
        // Đảm bảo bảng bắt đầu ở trạng thái ẩn
        if (diePanel != null)
        {
            diePanel.SetActive(false);
        }

        // Ẩn con trỏ chuột và khóa nó khi bắt đầu game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowDiePanel()
    {
        if (diePanel != null)
        {
            diePanel.SetActive(true);  // Hiển thị bảng "Die Panel"
            Time.timeScale = 0f;      // Dừng thời gian (Pause)

            // Hiển thị con trỏ chuột
            Cursor.lockState = CursorLockMode.None; // Mở khóa con trỏ
            Cursor.visible = true;                 // Hiện con trỏ
        }
    }

    public void GoToMainMenu()
    {
        // Khôi phục trạng thái bình thường trước khi chuyển Scene
        Time.timeScale = 1f; // Tiếp tục thời gian

        // Chuyển sang Scene "Main Menu"
        SceneManager.LoadScene("MainMenu"); // Thay "MainMenu" bằng tên Scene Main Menu của bạn
    }
}