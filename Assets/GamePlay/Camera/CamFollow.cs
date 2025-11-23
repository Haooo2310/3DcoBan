using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;       // Player
    public Transform cam;          // Main Camera
    public float mouseSensitivity = 150f;
    public float distance = 3f;    // Khoảng cách camera
    public float minY = -30f;      // Giới hạn nhìn xuống
    public float maxY = 60f;       // Giới hạn nhìn lên

    private float rotX = 0f;
    private bool mouseLocked = true;

    void Start()
    {
        LockMouse(true);
    }

    void Update()
    {
        // Toggle chuột bằng Alt
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            mouseLocked = !mouseLocked;
            LockMouse(mouseLocked);
        }

        if (!mouseLocked) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Xoay ngang
        transform.Rotate(Vector3.up * mouseX);

        // Xoay dọc camera
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, minY, maxY);

        cam.localRotation = Quaternion.Euler(rotX, 0f, 0f);

        // Camera giữ khoảng cách cố định
        cam.position = transform.position - cam.forward * distance;
    }

    void LockMouse(bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
