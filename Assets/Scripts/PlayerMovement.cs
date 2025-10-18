using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace scripts
{
    public class NewInputPlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float gravity;
        public float jumpHeight;

        private CharacterController _characterController;
        public PlayerInput PlayerControls;
        private InputAction moveAction;
        private InputAction jumpAction;
        public Transform cameraTransform;
        public Transform orientation;

        private Vector3 playerVelocity;
        private bool isGrounded;
        private bool jumpPressed;

        void Start()
        {
            PlayerControls = GetComponent<PlayerInput>();
            moveAction = PlayerControls.actions.FindAction("Move");
            _characterController = GetComponent<CharacterController>();
            jumpAction = PlayerControls.actions.FindAction("Jump");
            if (jumpAction != null)
            {
                jumpAction.performed += OnJumpPerformed;
                jumpAction.canceled += OnJumpCanceled;
            }
        }

        void Update()
        {
            GroundCheck();
            MovePlayer();
            ApplyGravity();
            HandleJump();
            transform.rotation = orientation.rotation;
        }

        void MovePlayer()
        {
            Vector2 direction = moveAction.ReadValue<Vector2>();
            Vector3 velocity = new Vector3(direction.x, 0, direction.y);
            velocity = -direction.x * cameraTransform.right.normalized + direction.y * cameraTransform.forward.normalized;
            if (isGrounded)
            {
                velocity.y = 0;
                if (playerVelocity.y != 0)
                {
                    velocity.y = playerVelocity.y;
                }
                playerVelocity = velocity;
            }
            _characterController.Move(playerVelocity * Time.deltaTime * moveSpeed);
        }
        void GroundCheck()
        {
            isGrounded = _characterController.isGrounded;
            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
        }
        void HandleJump()
        {
            if (jumpPressed && isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(gravity));
                jumpPressed = false;
            }
        }

        void ApplyGravity()
        {
            playerVelocity.y += gravity * Time.deltaTime;
            _characterController.Move(playerVelocity * Time.deltaTime);
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            jumpPressed = true;
        }
        private void OnJumpCanceled(InputAction.CallbackContext context)
        {
            jumpPressed = false;
        }

        void OnEnable()
        {
            if (jumpAction != null)
            {
                jumpAction.Enable();
            }
        }
        void OnDisable()
        {
            if (jumpAction != null)
            {
                jumpAction.Disable();
            }
            if (jumpAction != null)
            {
                jumpAction.performed -= OnJumpPerformed;
                jumpAction.canceled -= OnJumpCanceled;
            }
        }

        void OnDestroy()
        {
            if (jumpAction != null)
            {
                jumpAction.performed -= OnJumpPerformed;
                jumpAction.canceled -= OnJumpCanceled;
            }
        }
    }
}