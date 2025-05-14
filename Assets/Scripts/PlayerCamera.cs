using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float Rotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Rotation -= mouseY;

        Rotation = Mathf.Clamp(Rotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(Rotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}

