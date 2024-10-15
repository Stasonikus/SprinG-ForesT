using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerCharecterController : MonoBehaviour
    {
        #region Переменные
        private CharacterController controller;
        private Animator _anim;

        public float walkSpeed = 12f;  // Обычная скорость
        public float runSpeed = 18f;   // Скорость бега
        private float currentSpeed;    // Текущая скорость

        public float gravity = -9.81f * 2;
        public float jumpHeight = 3f;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        Vector3 velocity;

        bool isGrounded;
        bool isMoving;

        private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
        #endregion

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            currentSpeed = walkSpeed; // Изначально скорость — обычная

            _anim = GetComponent<Animator>(); // Getting Animation
        }

        private void Update()
        {
            // Проверка касания земли
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            // Сброс скорости по умолчанию
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // Получение входных данных
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");


            // ANIM
            if(Input.GetKey(KeyCode.W))
            {
                _anim.SetBool("isRunning", true); // Start animation run
            }
            else
            {
                _anim.SetBool("isRunning", false); // Stop animation run
            }
            // ANIM



            // Проверка нажатия клавиши Shift для ускорения
            if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
            {
                currentSpeed = runSpeed;  // Ускорение при удержании Shift
                _anim.SetBool("isSprinting", true); // Start animation sprint
            }
            else
            {
                currentSpeed = walkSpeed; // Возвращение к обычной скорости
                _anim.SetBool("isSprinting", false); // Start animation sprint
            }

            // Создание движущегося вектора
            Vector3 move = transform.right * x + transform.forward * z;

            // Актуальное перемещение игрока
            controller.Move(move * currentSpeed * Time.deltaTime);

            // Проверка прыжка
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);                
            }

            // Падение вниз
            velocity.y += gravity * Time.deltaTime;

            // Выполнение прыжка
            controller.Move(velocity * Time.deltaTime);
            _anim.SetTrigger("Jump"); // Start animation jump



            if (lastPosition != gameObject.transform.position && isGrounded == true)
            {
                isMoving = true;
                //_anim.SetBool("isRunning", true); // Start animation run
            }
            else
            {
                isMoving = false;
                //_anim.SetBool("isRunning", false); // Stop animation run
            }

            lastPosition = gameObject.transform.position;
        }
    }
}
