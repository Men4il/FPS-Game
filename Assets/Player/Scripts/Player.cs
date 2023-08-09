using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f; // Скорость движения
    [SerializeField] private float _gravity = -9.81f; // Гравитация для игрока
    [SerializeField] private float _jumpHeight = 2f; // Высота прыжка
    
    // Функции для доступа к переменным игрока из других скриптов
    public float GetMovementSpeed()
    {
        return _movementSpeed;
    }
    
    public float GetJumpHeight()
    {
        return _jumpHeight;
    }
    
    public float GetGravity()
    {
        return _gravity;
    }
}
