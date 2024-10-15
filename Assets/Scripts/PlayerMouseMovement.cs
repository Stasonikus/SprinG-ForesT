using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerMouseMovement : MonoBehaviour
    {
        #region ѕеременные
        public float mouseSensitivity = 100f;
 
        float yRotation = 0f;

        #endregion

        void Start () 
        {
            //Ћокаю курсор в начале как и хотел
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update ()
        {
            //вводные данные дл€ мыши
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;      

            // rotation влево вправо
            yRotation += mouseX;

            // применить вращение к нашему преобразованию
            transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);



            // «апрет вращени€: оставл€ем только изменение по оси Y (например, поворот влево-вправо)
            Vector3 currentRotation = transform.eulerAngles;
            currentRotation.x = 0; // Ѕлокируем вращение по оси X      
            transform.eulerAngles = currentRotation;
        }
    }
}
