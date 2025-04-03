using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroWithInstantSceneSwitch : MonoBehaviour
{
    [Header("Text Components")]
    public TMP_Text introText;        // TextMeshPro cho phần giới thiệu
    public TMP_Text loadingText;     // TextMeshPro cho "Loading..."
    
    [Header("Settings")]
    public float typingSpeed = 0.05f; // Tốc độ xuất hiện từng chữ
    public float dotDelay = 0.5f;     // Thời gian thêm dấu chấm
    public string sceneToLoad;        // Tên Scene cần tải
    
    [Header("Audio")]
    public AudioSource backgroundMusic; // Nhạc nền phát khi giới thiệu
    private string introFullText = "Bạn bị mắc kẹt trong một ngôi nhà tối tăm, hãy giải mã bí ẩn để thoát."; // Văn bản giới thiệu
    private bool isSceneLoading = false; // Theo dõi trạng thái tải Scene

    void Start()
    {
        // Chuẩn bị văn bản ban đầu
        introText.text = ""; // Xóa văn bản giới thiệu ban đầu
        loadingText.text = ""; // Xóa văn bản "Loading..." ban đầu

        // Phát nhạc nền nếu có
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        // Hiển thị giới thiệu
        StartCoroutine(ShowIntroText());
    }

    IEnumerator ShowIntroText()
    {
        // Hiển thị từng chữ trong giới thiệu
        for (int i = 0; i < introFullText.Length; i++)
        {
            introText.text += introFullText[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        // Tạm dừng một chút sau khi giới thiệu hoàn tất
        yield return new WaitForSeconds(1f);

        // Kết thúc nhạc nền (nếu muốn)
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        // Chuyển sang hiệu ứng "Loading..."
        StartCoroutine(LoadingTextAnimation());
    }

    IEnumerator LoadingTextAnimation()
    {
        // Hiệu ứng chữ "Loading..." chạy
        loadingText.text = "Loading";
        for (int i = 0; i < 3; i++)
        {
            loadingText.text += ".";
            yield return new WaitForSeconds(dotDelay);
        }

        // Chuyển Scene ngay sau khi Loading xong
        SceneManager.LoadScene(sceneToLoad);
    }
}