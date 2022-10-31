using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace inSession
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMovement : MonoBehaviour, IInputListener
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float acceleration = 5;
        [SerializeField] private float deceleration = 5;
        [SerializeField] private float angularDampening = 20;

        [SerializeField] private float jumpSpeed = 5;
        [SerializeField] private Vector3 airboneCheckStart;
        [SerializeField] private Vector3 airboneCheckDirection;
        [SerializeField] private float airboneCheckDistance;
        [SerializeField] private LayerMask airboneCheckMask;
        [SerializeField] private float LowJumpGravity;

        private Vector2 inputVector;
        private Vector2 wetInputVector;
        private Vector3 lastFoward;

        private Rigidbody rigidbody;
        private float jumpValue;

        private bool airbone
        {
            get
            {
                Ray r = new Ray(transform.TransformPoint(airboneCheckStart), 
                    transform.TransformDirection(airboneCheckDirection));
                return !Physics.Raycast(r, airboneCheckDistance, airboneCheckMask);
            }
        }

        public void Move(InputAction.CallbackContext context)
        {
            if (context.action.name != "Motion") return;
            inputVector = context.ReadValue<Vector2>();
           
        }
        public void Jump(InputAction.CallbackContext context)
        {
            if (context.action.name != "Jump") return;
            jumpValue = context.ReadValue<float>();
            if (!airbone) rigidbody.AddForce(Vector3.up * jumpSpeed * jumpValue, ForceMode.VelocityChange);
        }
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        void DampenImput()
        {
            float x = wetInputVector.x;
            float y = wetInputVector.y;
            if (inputVector.x == 0)
            {
                x = Mathf.MoveTowards(x, 0f, Time.deltaTime * deceleration);
            }
            else
            {
                x = Mathf.MoveTowards(x, inputVector.x, Time.deltaTime * acceleration);
            }

            if(inputVector.y == 0)
            {
                y = Mathf.MoveTowards(y, 0f, Time.deltaTime * deceleration);
            }
            else
            {
                y = Mathf.MoveTowards(y, inputVector.y, Time.deltaTime * acceleration);
            }
            wetInputVector.Set(x, y);
        }

        private void FixedUpdate()
        {
            DampenImput();

            if (rigidbody.velocity.y < 0)
            {
                rigidbody.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
            }
            else if(rigidbody.velocity.y > 0 && jumpValue <= 0.1f)
            {
                rigidbody.velocity += Vector3.up * Physics.gravity.y * LowJumpGravity * Time.fixedDeltaTime;
            }

            Transform cameraTransform = Camera.main.transform;
            Vector3 right = Vector3.ProjectOnPlane(cameraTransform.right, transform.up);
            Vector3 foward = Vector3.ProjectOnPlane(cameraTransform.forward, transform.up);
            Vector3 motionVector = right * (wetInputVector.x * movementSpeed * Time.fixedDeltaTime) + 
                    foward * (wetInputVector.y * movementSpeed * Time.fixedDeltaTime);
            transform.Translate(motionVector, Space.World );
            if(motionVector.magnitude > 0)
            {
                transform.forward = Vector3.Slerp(lastFoward.normalized, motionVector.normalized, Time.fixedDeltaTime * angularDampening);
            }
            lastFoward = transform.forward;
        }

        public Action<InputAction.CallbackContext>[] ListenerFunctions => new Action<InputAction.CallbackContext>[] { Move, Jump };

        private void OnDrawGizmos()
        {
            Gizmos.color = airbone ? Color.red : Color.green;
            Vector3 rayStart = transform.TransformPoint(airboneCheckStart);
            //Gizmos.DrawLine();
        }
    }
}

