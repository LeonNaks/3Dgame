using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Để chuyển Scene

public class TextRevealWithSoundAndScene : MonoBehaviour
{
    public TMP_Text textMeshPro;       // Tham chiếu đến TextMeshPro
    public float typingSpeed = 0.05f;  // Thời gian giữa mỗi ký tự
    public AudioSource typingSound;    // Âm thanh gõ phím
    public string sceneToLoad;         // Tên Scene cần tải

    private string fullText;           // Chuỗi toàn bộ văn bản
    private string currentText = "";   // Chuỗi hiển thị hiện tại

    void Start()
    {
        // Lấy nội dung của TextMeshPro
        if (textMeshPro != null)
        {
            fullText = textMeshPro.text;
            textMeshPro.text = ""; // Bắt đầu với văn bản trống
            if (typingSound != null)
            {
                typingSound.loop = true; // Đặt âm thanh lặp lại
            }
            StartCoroutine(RevealText());
        }
    }

    IEnumerator RevealText()
    {
        // Bắt đầu phát âm thanh
        if (typingSound != null)
        {
            typingSound.Play();
        }

        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i]; // Thêm ký tự vào chuỗi hiển thị
            textMeshPro.text = currentText; // Cập nhật TextMeshPro

            yield return new WaitForSeconds(typingSpeed); // Đợi trước khi thêm ký tự tiếp theo
        }

        // Dừng âm thanh khi kết thúc
        if (typingSound != null)
        {
            typingSound.Stop();
        }

        // Đợi một chút trước khi chuyển Scene
        yield return new WaitForSeconds(1f); 
        SceneManager.LoadScene(sceneToLoad); // Chuyển sang Scene mới
    }
}