using UnityEngine;

public class DynamicCameraShake : MonoBehaviour
{
    // Настройки для раскачивания
    public Transform cameraTransform;  // Ссылка на камеру
    public float bobSpeed = 6f;        // Скорость раскачивания (частота шагов)
    public float bobAmount = 0.05f;    // Амплитуда покачивания по вертикали
    public float swayAmount = 0.03f;   // Амплитуда раскачивания по горизонтали
    public float smoothReturnSpeed = 2f; // Скорость возврата камеры в исходное положение
    public float jumpImpact = 0.1f;    // Сила толчка при прыжке
    public float jumpRecoverySpeed = 4f; // Скорость восстановления камеры после прыжка

    // Приватные переменные
    private Vector3 originalPos;       // Исходное положение камеры
    private float timer = 0f;          // Таймер для расчета синусоиды
    private bool isMoving = false;     // Проверка движения персонажа
    private float stopTimer = 0f;      // Таймер для плавной остановки движения камеры
    private bool isJumping = false;    // Флаг для проверки прыжка
    private float jumpOffset = 0f;     // Смещение камеры при прыжке

    void Start()
    {
        // Если камера не назначена, используем текущий Transform
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }

        // Запоминаем исходное положение камеры
        originalPos = cameraTransform.localPosition;
    }

    void Update()
    {
        // Проверяем движение персонажа (движение вперед, влево, назад, вправо)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            stopTimer = 0f; // Сбрасываем таймер остановки
            timer += Time.deltaTime * bobSpeed; // Увеличиваем таймер для плавного раскачивания
        }
        else
        {
            isMoving = false;
            stopTimer += Time.deltaTime; // Увеличиваем таймер остановки, чтобы плавно завершить тряску
        }

        // Обрабатываем прыжок
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            jumpOffset = jumpImpact; // Применяем толчок вверх
        }

        // Если персонаж прыгает, постепенно уменьшаем смещение камеры
        if (isJumping)
        {
            jumpOffset = Mathf.Lerp(jumpOffset, 0f, Time.deltaTime * jumpRecoverySpeed);

            // Если смещение почти исчезло, считаем прыжок завершенным
            if (Mathf.Abs(jumpOffset) < 0.01f)
            {
                isJumping = false;
            }
        }

        // Выполняем тряску камеры, если персонаж двигается
        if (isMoving)
        {
            ApplyCameraBob();
        }
        else if (stopTimer < 1f)  // Продолжение тряски после остановки на небольшое время
        {
            ApplyCameraBob();
        }
        else
        {
            // Плавно возвращаем камеру в исходное положение
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, originalPos, Time.deltaTime * smoothReturnSpeed);
        }
    }

    // Функция для применения раскачивания камеры
    void ApplyCameraBob()
    {
        // Генерируем мягкое колебание по вертикали и горизонтали
        float verticalOffset = Mathf.Sin(timer) * bobAmount + jumpOffset; // Мягкое движение вверх и вниз + толчок при прыжке
        float horizontalOffset = Mathf.Cos(timer) * swayAmount; // Легкое колебание влево-вправо

        // Применяем смещение к камере
        cameraTransform.localPosition = new Vector3(
            originalPos.x + horizontalOffset,
            originalPos.y + verticalOffset,
            originalPos.z
        );
    }
}
