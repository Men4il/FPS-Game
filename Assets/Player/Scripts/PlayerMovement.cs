using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControlsInitializer _initializer;
    private CharacterController _controller;
    private Player _player;
    
    private Vector3 _playerPosition;
    private Vector3 _playerVelocity;
    private bool _playerGrounded;
    private Transform _cameraTransform;
    
    void Start()
    {
        // Присваиваем переменным соответственные объекты
        _player = GetComponent<Player>();
        _initializer = GetComponent<PlayerControlsInitializer>();
        _playerPosition = _initializer.MoveInput;
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;

        // Скрываем курсор
        Cursor.visible = false;
    }
    
    void Update()
    {
        Move();
    }
    
    private void Move()
    {
        _playerGrounded = _controller.isGrounded; // Проверяем, на земле ли игрок
        if (_playerGrounded && _playerVelocity.y < 0)
        {
            // Если игрок на земле и не падает, то ставим его инерции по координате y значение, равное нулю
            _playerVelocity.y = 0f;
        }
        
        _playerPosition = new Vector3(_initializer.MoveInput.x, 0, _initializer.MoveInput.y); // Если игрок начал движение, то по координатам x, z считываем на сколько он должен сдвинутся
        _playerPosition = _cameraTransform.forward * _playerPosition.z + _cameraTransform.right * _playerPosition.x; // Считаем следующее положение игрока
        _playerPosition.y = 0f;
        _controller.Move( _playerPosition * (_player.GetMovementSpeed() * Time.deltaTime)); // Двигаем игрока

        if (_initializer.JumpInput && _playerGrounded)
        {
            // Проверяем, если игрок на земле и нажал прижок, то по формуле прыжка увеличиваем инерцию игрока по координате y
            _playerVelocity.y = Mathf.Sqrt(_player.GetJumpHeight() * -3f * _player.GetGravity());
        }

        _playerVelocity.y += _player.GetGravity() * Time.deltaTime; // Применяем гравитацию к игроку
        _controller.Move(_playerVelocity * Time.deltaTime); // Двигаем игрока, если он двигается по инерции
    }
}
