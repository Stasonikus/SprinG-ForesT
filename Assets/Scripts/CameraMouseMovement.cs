using UnityEngine;

namespace PlayerControl
{
    public class CameraMouseMovement : MonoBehaviour
    {
        public float Yrotation = -90f;
        public float Zrotation = 90f;

        public Transform playerBody; // ������ �� ������ ������
        private float xRotation = 0f;
        public PlayerMouseMovement playerMouseMovement;

        void Start()
        {
            playerMouseMovement = FindObjectOfType<PlayerMouseMovement>();
            // �������� ������ ���� � ���� � ��������� ��� � ������ ������
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            
            if (playerMouseMovement != null)
            {
                // �������� �������� ���� �� ��� Y (����� � ����)
                float mouseY = Input.GetAxis("Mouse Y") * playerMouseMovement.mouseSensitivity * Time.deltaTime;
                // ������������ �������� �� ��� X, ����� ������ �� ����������������
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, Yrotation, Zrotation); // ������������ ���� �� -90 �� 90 ��������

                // ��������� �������� � ������
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }
            else
            {
                Debug.LogError("��� ������ �� ������ �������� ����� �����");
            }


        }
    }
}
