using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
        [SerializeField] private float moveSpeed = 3f;
        private CharacterController controller;
        
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (Keyboard.current is null)
            {
                return;
            }

            Vector2 input = Vector2.zero;

            if (Keyboard.current.wKey.isPressed)
            {
                input.y += 1f;
            }
            
            if (Keyboard.current.sKey.isPressed)
                input.y -= 1f;

            if (Keyboard.current.aKey.isPressed)
                input.x -= 1f;

            if (Keyboard.current.dKey.isPressed)
                input.x += 1f;
            
            input = Vector2.ClampMagnitude(input, 1f);
            
            Vector3 movement = transform.right * input.x + transform.forward * input.y;
            
             controller.Move(movement * moveSpeed * Time.deltaTime);
        }
}
