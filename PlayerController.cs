using UnityEngine;

namespace EclipseProtocol
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public Transform cameraPivot;
        public float moveSpeed = 6f;
        public float sprintMultiplier = 1.5f;
        public float jumpHeight = 1.2f;
        public float gravity = -20f;
        public float mouseSensitivity = 1.5f;
        public float maxLookAngle = 85f;

        public Gun gun;

        private CharacterController cc;
        private Vector3 velocity;
        private float pitch;
        private bool cursorLocked = true;

        void Awake()
        {
            cc = GetComponent<CharacterController>();
            LockCursor(true);
        }

        void Update()
        {
            HandleLook();
            HandleMove();
            HandleActions();

            if (Input.GetKeyDown(KeyCode.Escape))
                LockCursor(!cursorLocked);
        }

        void HandleLook()
        {
            float mx = Input.GetAxis("Mouse X") * mouseSensitivity * 5f * Time.deltaTime * 60f;
            float my = Input.GetAxis("Mouse Y") * mouseSensitivity * 5f * Time.deltaTime * 60f;

            transform.Rotate(0f, mx, 0f);

            pitch -= my;
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
            if (cameraPivot) cameraPivot.localEulerAngles = new Vector3(pitch, 0f, 0f);
        }

        void HandleMove()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 input = new Vector3(h, 0f, v);
            input = Vector3.ClampMagnitude(input, 1f);

            float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f);
            Vector3 world = transform.TransformDirection(input) * speed;

            // gravity & jump
            if (cc.isGrounded)
            {
                velocity.y = -2f;
                if (Input.GetButtonDown("Jump"))
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            velocity.y += gravity * Time.deltaTime;

            cc.Move((world + new Vector3(0, velocity.y, 0)) * Time.deltaTime);
        }

        void HandleActions()
        {
            if (gun == null) return;
            if (Input.GetMouseButton(0)) gun.TryFire();
            if (Input.GetKeyDown(KeyCode.R)) gun.Reload();
        }

        void LockCursor(bool locked)
        {
            cursorLocked = locked;
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !locked;
        }
    }
}
