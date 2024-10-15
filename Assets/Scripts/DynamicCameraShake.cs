using UnityEngine;

public class DynamicCameraShake : MonoBehaviour
{
    // ��������� ��� ������������
    public Transform cameraTransform;  // ������ �� ������
    public float bobSpeed = 6f;        // �������� ������������ (������� �����)
    public float bobAmount = 0.05f;    // ��������� ����������� �� ���������
    public float swayAmount = 0.03f;   // ��������� ������������ �� �����������
    public float smoothReturnSpeed = 2f; // �������� �������� ������ � �������� ���������
    public float jumpImpact = 0.1f;    // ���� ������ ��� ������
    public float jumpRecoverySpeed = 4f; // �������� �������������� ������ ����� ������

    // ��������� ����������
    private Vector3 originalPos;       // �������� ��������� ������
    private float timer = 0f;          // ������ ��� ������� ���������
    private bool isMoving = false;     // �������� �������� ���������
    private float stopTimer = 0f;      // ������ ��� ������� ��������� �������� ������
    private bool isJumping = false;    // ���� ��� �������� ������
    private float jumpOffset = 0f;     // �������� ������ ��� ������

    void Start()
    {
        // ���� ������ �� ���������, ���������� ������� Transform
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }

        // ���������� �������� ��������� ������
        originalPos = cameraTransform.localPosition;
    }

    void Update()
    {
        // ��������� �������� ��������� (�������� ������, �����, �����, ������)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            stopTimer = 0f; // ���������� ������ ���������
            timer += Time.deltaTime * bobSpeed; // ����������� ������ ��� �������� ������������
        }
        else
        {
            isMoving = false;
            stopTimer += Time.deltaTime; // ����������� ������ ���������, ����� ������ ��������� ������
        }

        // ������������ ������
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            jumpOffset = jumpImpact; // ��������� ������ �����
        }

        // ���� �������� �������, ���������� ��������� �������� ������
        if (isJumping)
        {
            jumpOffset = Mathf.Lerp(jumpOffset, 0f, Time.deltaTime * jumpRecoverySpeed);

            // ���� �������� ����� �������, ������� ������ �����������
            if (Mathf.Abs(jumpOffset) < 0.01f)
            {
                isJumping = false;
            }
        }

        // ��������� ������ ������, ���� �������� ���������
        if (isMoving)
        {
            ApplyCameraBob();
        }
        else if (stopTimer < 1f)  // ����������� ������ ����� ��������� �� ��������� �����
        {
            ApplyCameraBob();
        }
        else
        {
            // ������ ���������� ������ � �������� ���������
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, originalPos, Time.deltaTime * smoothReturnSpeed);
        }
    }

    // ������� ��� ���������� ������������ ������
    void ApplyCameraBob()
    {
        // ���������� ������ ��������� �� ��������� � �����������
        float verticalOffset = Mathf.Sin(timer) * bobAmount + jumpOffset; // ������ �������� ����� � ���� + ������ ��� ������
        float horizontalOffset = Mathf.Cos(timer) * swayAmount; // ������ ��������� �����-������

        // ��������� �������� � ������
        cameraTransform.localPosition = new Vector3(
            originalPos.x + horizontalOffset,
            originalPos.y + verticalOffset,
            originalPos.z
        );
    }
}
