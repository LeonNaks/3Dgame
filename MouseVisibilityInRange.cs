using UnityEngine;

public class MouseVisibilityInRange : MonoBehaviour
{
    public Transform areaCenter; // Tâm của phạm vi
    public float range = 5f; // Bán kính phạm vi hiển thị chuột

    private bool isInRange = false;

    void Update()
    {
        // Kiểm tra vị trí của con trỏ chuột (camera với raycast)
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Tính khoảng cách giữa chuột và tâm khu vực
        float distance = Vector3.Distance(worldMousePosition, areaCenter.position);

        // Nếu chuột nằm trong phạm vi
        if (distance <= range)
        {
            if (!isInRange)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isInRange = true;
                Debug.Log("Chuột Active / Small Locked Right Limits");
            }
        }
    }
}