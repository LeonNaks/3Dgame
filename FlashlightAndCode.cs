using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashlightAndCode : MonoBehaviour
{
    public Light flashlight; // Đèn pin
    public TMP_Text codeText; // Văn bản hiển thị mật mã
    public string secretCode = "326746"; // Mật mã

    private void Update()
    {
        // Kiểm tra xem người chơi có bật đèn pin hay không
        if (flashlight.enabled)
        {
            ShowCode(); // Hiển thị mật mã nếu đèn pin bật
        }
        else
        {
            HideCode(); // Ẩn mật mã nếu đèn pin tắt
        }
    }

    private void ShowCode()
    {
        codeText.text = secretCode; // Hiển thị mật mã
    }

    private void HideCode()
    {
        codeText.text = ""; // Ẩn mật mã
    }
}