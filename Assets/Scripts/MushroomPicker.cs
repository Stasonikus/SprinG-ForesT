using UnityEngine;

public class MushroomPicker : MonoBehaviour
{
    public Camera mainCamera; // Ссылка на основную камеру
    public GameObject eatButton; // Кнопка "Съесть"
    public float pickupDistance = 3f; // Максимальная дистанция для съедания гриба
    private GameObject currentMushroom; // Хранит текущий гриб

    void Start()
    {
        // Скрываем кнопку в начале
        eatButton.SetActive(false);
    }

    void Update()
    {
        // Создаем луч из центра экрана
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Проверяем, попадает ли луч в гриб
        if (Physics.Raycast(ray, out hit))
        {
            // Проверяем, является ли объект грибом
            if (hit.collider.CompareTag("Mushroom"))
            {
                // Получаем объект гриба
                currentMushroom = hit.collider.gameObject;

                // Вычисляем дистанцию между игроком (камерой) и грибом
                float distanceToMushroom = Vector3.Distance(mainCamera.transform.position, currentMushroom.transform.position);

                // Если расстояние меньше или равно допустимому расстоянию для подбора
                if (distanceToMushroom <= pickupDistance)
                {
                    // Выделяем гриб
                    HighlightMushroom(currentMushroom);

                    // Если нажата клавиша "E", съедаем гриб
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        EatMushroom(currentMushroom);
                    }
                }
                else
                {
                    // Если расстояние больше, убираем выделение
                    ResetMushroomHighlight(currentMushroom);
                }
            }
            else
            {
                // Если курсор не на грибе, убираем выделение
                if (currentMushroom != null)
                {
                    ResetMushroomHighlight(currentMushroom);
                    currentMushroom = null;
                }
            }
        }
        else
        {
            // Если луч не попадает ни в один объект, убираем выделение
            if (currentMushroom != null)
            {
                ResetMushroomHighlight(currentMushroom);
                currentMushroom = null;
            }
        }
    }

    private void HighlightMushroom(GameObject mushroom)
    {
        // Здесь можно добавить логику выделения, например, изменить цвет
        mushroom.GetComponent<Renderer>().material.color = Color.yellow; // Изменение цвета на желтый
        eatButton.SetActive(true); // Показываем кнопку "Съесть"
    }

    private void ResetMushroomHighlight(GameObject mushroom)
    {
        // Сбрасываем цвет гриба на исходный
        mushroom.GetComponent<Renderer>().material.color = Color.white; // Или любой другой исходный цвет
        eatButton.SetActive(false); // Скрываем кнопку "Съесть"
    }

    private void EatMushroom(GameObject mushroom)
    {
        // Логика поедания гриба
        Debug.Log("Съеден гриб: " + mushroom.name);
        Destroy(mushroom); // Удаляем гриб из сцены
    }
}
