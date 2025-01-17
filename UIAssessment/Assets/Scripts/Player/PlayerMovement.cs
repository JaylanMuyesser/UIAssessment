﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    [AddComponentMenu("Game Systems/RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]

    public class PlayerMovement : MonoBehaviour
    {
        [Header("Physics")]
        public float gravity = 20f;
        public CharacterController controller;
        [Header("Movement Variables")]
        public float speed = 8f;
        public float jumpSpeed = 8f;
        public float sprintSpeed = 15f;
        public float crouchSpeed = 4f;
        public Vector3 moveDirection;

        //Bruh Moment
        void Start()
        {
            //Grabs the character controller attatched to the object
            controller = GetComponent<CharacterController>();

        }

        void Update()
        {
            float horizontal = 0;
            float vertical = 0;
            if(Input.GetKey(KeybindManager.keys["Forward"]))
            {
                vertical++;
            }
            if (Input.GetKey(KeybindManager.keys["Left"]))
            {
                horizontal--;
            }
            if (Input.GetKey(KeybindManager.keys["Right"]))
            {
                horizontal++;
            }
            if (Input.GetKey(KeybindManager.keys["Backward"]))
            {
                vertical--;
            }
            if (controller.isGrounded)
            {
                moveDirection = transform.TransformDirection(new Vector3(horizontal, 0f, vertical));
                moveDirection *= speed;
                if (Input.GetKey(KeybindManager.keys["Jump"]))
                {
                    moveDirection.y = jumpSpeed;
                }
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 8f;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                speed = crouchSpeed;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                speed = 8f;
            }
        }
    }

}
