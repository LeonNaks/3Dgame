using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingEffect : MonoBehaviour
{
    public TMP_Text loadingText;          // TextMeshPro cho chữ "Loading..."
    public float typingSpeed = 0.2f;      // Tốc độ hiển thị mỗi ký tự
    public string sceneToLoad;            // Tên Scene cần tải
    public float dotDelay = 0.5f;         // Thời gian để thêm dấu "." vào "Loading"

    private bool isSceneLoading = false;  // Kiểm tra nếu Scene đang tải

    void Start()
    {
        // Bắt đầu hiệu ứng chữ "Loading..."
        StartCoroutine(LoadingTextAnimation());
        // Bắt đầu tải Scene
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadingTextAnimation()
    {
        while (!isSceneLoading)
        {
            loadingText.text = "Loading"; // Cài đặt văn bản cơ bản
            for (int i = 0; i < 3; i++)   // Thêm lần lượt ba dấu chấm
            {
                loadingText.text += ".";
                yield return new WaitForSeconds(dotDelay);
            }
        }
    }

    IEnumerator LoadSceneAsync()
    {
        // Bắt đầu tải Scene bất đồng bộ
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false; // Chưa cho phép chuyển ngay lập tức

        while (!operation.isDone)
        {
            // Kiểm tra nếu tiến trình tải đạt mức 90%
            if (operation.progress >= 0.9f)
            {
                isSceneLoading = true; // Kết thúc hiệu ứng Loading
                yield return new WaitForSeconds(1f); // Thêm chút thời gian chờ
                operation.allowSceneActivation = true; // Kích hoạt Scene mới
            }
            yield return null;
        }
    }
}