using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 100f; // Độ nhạy của chuột
    public Transform playerBody;     // Kéo đối tượng Player vào đây trong Inspector

    float xRotation = 0f;

    void Start()
    {
        // Ẩn con trỏ chuột và khóa nó ở giữa màn hình
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()   
    {
        // 1. Lấy đầu vào chuột
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // 2. Xoay Camera theo chiều dọc (Nhìn lên/xuống)
        // Camera xoay quanh trục X (pitch)
        xRotation -= mouseY;
        // Giới hạn góc nhìn để không bị lật (ví dụ: -90 đến 90 độ)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Áp dụng góc xoay cho Camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 3. Xoay Player theo chiều ngang (Xoay người)
        // Player xoay quanh trục Y (yaw)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}