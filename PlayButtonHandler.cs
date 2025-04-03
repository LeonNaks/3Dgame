using UnityEngine;
using System.Collections; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    public GameObject loadingPanel; // Panel Loading
    public Slider progressBar;      // Thanh tiến trình tải
    public string sceneToLoad;      // Tên Scene cần tải

    void Start()
    {
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false); // Đảm bảo Panel Loading bắt đầu ở trạng thái tắt
        }
    }

    public void OnPlayButtonClicked()
    {
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true); // Hiển thị Panel Loading
        }

        // Bắt đầu Coroutine để tải Scene bất đồng bộ
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        // Bắt đầu tải Scene bất đồng bộ
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false; // Ngăn Scene tự động kích hoạt

        while (!operation.isDone)
        {
            // Cập nhật tiến trình
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
            {
                progressBar.value = progress; // Cập nhật thanh tiến trình
            }

            // Khi tiến trình đạt 90%, cho phép chuyển Scene
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null; // Đợi mỗi khung hình
        }
    }
}