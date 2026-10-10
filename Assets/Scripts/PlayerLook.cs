using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
        [SerializeField] private float sensitivity = 0.1f;
        [SerializeField] private float minPitch = -80f;
        [SerializeField] private float maxPitch = 80f;
    
        private float yaw;
        private float pitch;
    
        private void Start()
        {
            if (cameraTransform == null)
            {
                Debug.LogError("PlayerLook: assign Main Camera in Inspector.", this);
                enabled = false;
                return;
            }
    
            yaw = transform.eulerAngles.y;
            pitch = cameraTransform.localEulerAngles.x;
    
            if (pitch > 180f)
                pitch -= 360f;
    
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    
        private void Update()
        {
            if (Mouse.current == null)
                return;
    
            if (Keyboard.current != null &&
                Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                return;
            }
    
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
    
                return;
            }
    
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
    
            yaw += mouseDelta.x * sensitivity;
            pitch -= mouseDelta.y * sensitivity;
    
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    
            transform.rotation = Quaternion.Euler(0f, yaw, 0f);
            cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
}
