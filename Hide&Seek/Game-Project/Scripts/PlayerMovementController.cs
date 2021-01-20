using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NocVedcu
{
    public class PlayerMovementController : MonoBehaviour
    {
        public float rotationLimit = 60f;

        //Nastavitelnr promenne
        public CharacterController characterController = null;
        public Transform cameraPoint = null;
        public Camera camera = null;

        public float runSpeed = 4f;
        public float sprintSpeed = 10f;
        public float gravity = 12;
        public float crouchSprintSpeed = 4f;
        public float crounchSpeed = 1f;
        private float jumpHeight = 3f;

        //privatni promenne
        private float rotationY = 0f;
        private float rotationX = 0f;
        private Vector3 move;
        private bool _IsCrouching = false;
        private bool _IsSprinting = false;
        public bool stopMove = false;

        private void Start() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update() {
            if (!stopMove) {
                float vertical = Input.GetAxis("Vertical");
                float horizontal = Input.GetAxis("Horizontal");

                move.x = horizontal;
                move.z = vertical;

                Vector3 tempMove = move;
                tempMove.y = 0f;
                tempMove = cameraPoint.TransformDirection(tempMove);

                move.x = tempMove.x;
                move.z = tempMove.z;

                TryJump();
                CalcHeight();
                float speed = CalcSpeed();

                if (characterController.isGrounded)
                {
                    if (move.y < -5f) move.y = -1f;
                }
                else
                {
                    move.y -= gravity * Time.deltaTime;
                }

                characterController.Move(move * (speed * Time.deltaTime));

                Vector3 forward = cameraPoint.forward;
                forward.y = 0f;
                transform.forward = forward;
            }
        }

        private void LateUpdate()
        {
            if (!stopMove || PauseMenu.gameIsPaused) {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                rotationX -= mouseY;
                rotationY += mouseX;

                if (rotationX > rotationLimit)
                {
                    rotationX = rotationLimit;
                }
                else if (rotationX < -rotationLimit)
                {
                    rotationX = -rotationLimit;
                }

                cameraPoint.rotation = Quaternion.Euler(rotationX, rotationY, 0);
                camera.transform.position = cameraPoint.position;
                camera.transform.rotation = cameraPoint.rotation;
            }
        }
        private void CalcHeight()
        {
            _IsCrouching = Input.GetKey(KeyCode.C);

            if (Input.GetKey(KeyCode.C))
            {
                characterController.height = 1f;
            }
            else
            {
                characterController.height = 2f;
            }
        }
        private float CalcSpeed()
        {
            _IsSprinting = Input.GetKey(KeyCode.LeftShift);
            float speed = runSpeed;
            if (_IsCrouching)
            {
                if (_IsSprinting)
                {
                    speed = crouchSprintSpeed;
                }
                else
                {
                    speed = crounchSpeed;
                }
            }
            else if (_IsSprinting)
            {
                speed = sprintSpeed;
            }
            return speed;
        }
        private void TryJump()
        {
            if (Input.GetButtonDown("Jump") && characterController.isGrounded)
            {
                move.y = jumpHeight;
            }
        }
    }
}
