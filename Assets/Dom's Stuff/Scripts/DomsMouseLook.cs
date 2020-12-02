using UnityEngine;

public class DomsMouseLook : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    private Transform PlayerBody;

    private float xRotation = 0f;

    private void Start()
    {
        PlayerBody = gameObject.transform.parent;

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        PlayerBody.Rotate(Vector3.up * MouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
