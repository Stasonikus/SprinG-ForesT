using UnityEngine;

public class MushroomPicker : MonoBehaviour
{
    public Camera mainCamera; // ������ �� �������� ������
    public GameObject eatButton; // ������ "������"
    public float pickupDistance = 3f; // ������������ ��������� ��� �������� �����
    private GameObject currentMushroom; // ������ ������� ����

    void Start()
    {
        // �������� ������ � ������
        eatButton.SetActive(false);
    }

    void Update()
    {
        // ������� ��� �� ������ ������
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ���������, �������� �� ��� � ����
        if (Physics.Raycast(ray, out hit))
        {
            // ���������, �������� �� ������ ������
            if (hit.collider.CompareTag("Mushroom"))
            {
                // �������� ������ �����
                currentMushroom = hit.collider.gameObject;

                // ��������� ��������� ����� ������� (�������) � ������
                float distanceToMushroom = Vector3.Distance(mainCamera.transform.position, currentMushroom.transform.position);

                // ���� ���������� ������ ��� ����� ����������� ���������� ��� �������
                if (distanceToMushroom <= pickupDistance)
                {
                    // �������� ����
                    HighlightMushroom(currentMushroom);

                    // ���� ������ ������� "E", ������� ����
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        EatMushroom(currentMushroom);
                    }
                }
                else
                {
                    // ���� ���������� ������, ������� ���������
                    ResetMushroomHighlight(currentMushroom);
                }
            }
            else
            {
                // ���� ������ �� �� �����, ������� ���������
                if (currentMushroom != null)
                {
                    ResetMushroomHighlight(currentMushroom);
                    currentMushroom = null;
                }
            }
        }
        else
        {
            // ���� ��� �� �������� �� � ���� ������, ������� ���������
            if (currentMushroom != null)
            {
                ResetMushroomHighlight(currentMushroom);
                currentMushroom = null;
            }
        }
    }

    private void HighlightMushroom(GameObject mushroom)
    {
        // ����� ����� �������� ������ ���������, ��������, �������� ����
        mushroom.GetComponent<Renderer>().material.color = Color.yellow; // ��������� ����� �� ������
        eatButton.SetActive(true); // ���������� ������ "������"
    }

    private void ResetMushroomHighlight(GameObject mushroom)
    {
        // ���������� ���� ����� �� ��������
        mushroom.GetComponent<Renderer>().material.color = Color.white; // ��� ����� ������ �������� ����
        eatButton.SetActive(false); // �������� ������ "������"
    }

    private void EatMushroom(GameObject mushroom)
    {
        // ������ �������� �����
        Debug.Log("������ ����: " + mushroom.name);
        Destroy(mushroom); // ������� ���� �� �����
    }
}
