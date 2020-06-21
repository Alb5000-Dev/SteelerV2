using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public static PlayerLook instance;
    [SerializeField] string mouseXInputName, mouseYInputName;
    public float mouseSensitivity;
    public float mouseSensitivitySet;

    private float xAxisClamp;

    [SerializeField] private Transform playerBody;

    private void Awake()
    {
        instance = this;
        LockCursor();
        xAxisClamp = 0.0f;
        mouseSensitivity = mouseSensitivitySet;
    }

    void LateUpdate()
    {
        CameraRotation();
    }

    public void resetSensitivity()
    {
        mouseSensitivity = mouseSensitivitySet;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void ChangeMouseSensitivity(float value)
    {
        mouseSensitivity = value;
    }

    private void CameraRotation()
    {      
        float mouseX = Input.GetAxisRaw(mouseXInputName) * mouseSensitivity;
        float mouseY = Input.GetAxisRaw(mouseYInputName) * mouseSensitivity;

        xAxisClamp += mouseY;

        if(xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
