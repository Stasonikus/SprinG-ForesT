using UnityEngine;

namespace PlayerControl
{
    public class CameraMouseMovement : MonoBehaviour
    {
        public float Yrotation = -90f;
        public float Zrotation = 90f;

        public Transform playerBody; // Ссылка на объект игрока
        private float xRotation = 0f;
        public PlayerMouseMovement playerMouseMovement;

        void Start()
        {
            playerMouseMovement = FindObjectOfType<PlayerMouseMovement>();
            // Скрываем курсор мыши в игре и блокируем его в центре экрана
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            
            if (playerMouseMovement != null)
            {
                // Получаем движение мыши по оси Y (вверх и вниз)
                float mouseY = Input.GetAxis("Mouse Y") * playerMouseMovement.mouseSensitivity * Time.deltaTime;
                // Ограничиваем вращение по оси X, чтобы камера не переворачивалась
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, Yrotation, Zrotation); // Ограничиваем угол от -90 до 90 градусов

                // Применяем вращение к камере
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }
            else
            {
                Debug.LogError("НЕТ ССЫЛКИ НА СКРИПТ ВРЕЩЕНИЯ ТЕЛОМ ПЕРСА");
            }


        }
    }
}
