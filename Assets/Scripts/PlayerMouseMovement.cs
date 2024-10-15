using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerMouseMovement : MonoBehaviour
    {
        #region ����������
        public float mouseSensitivity = 100f;
 
        float yRotation = 0f;

        #endregion

        void Start () 
        {
            //����� ������ � ������ ��� � �����
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update ()
        {
            //������� ������ ��� ����
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;      

            // rotation ����� ������
            yRotation += mouseX;

            // ��������� �������� � ������ ��������������
            transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);



            // ������ ��������: ��������� ������ ��������� �� ��� Y (��������, ������� �����-������)
            Vector3 currentRotation = transform.eulerAngles;
            currentRotation.x = 0; // ��������� �������� �� ��� X      
            transform.eulerAngles = currentRotation;
        }
    }
}
