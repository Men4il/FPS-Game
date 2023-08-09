using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PlayerControlsInitializer _playerControls;
    [SerializeField] private GameObject _playerStatsUI;
    [SerializeField] private Shooting _shootingLogic;
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _settingsMenuUI;

    public static bool _paused; // Переменная, отвечающая за состояние игры (стоит ли пауза)
    private Slider _mouseSensitivitySlider;
    private bool _pausePressed;
    private bool _settingsOpened;
    private CinemachinePOV _virtualCamera;

    private void OnEnable()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
        _mouseSensitivitySlider = GetComponentInChildren<Slider>(true);
        _playerControls.Pause += Pause;
    }
    
    private void OnDisable()
    {
        _playerControls.Pause -= Pause;
    }

    public void Pause()
    {
        if (!_paused)
        {
            _pauseMenuUI.SetActive(true); 
            _playerStatsUI.SetActive(false); 
            Time.timeScale = 0f; // Останавливаем время игры
            _paused = true; // Ставим состояние игры в режим паузы
            _shootingLogic.SetTimeStopped(true); // Передаём скрипту стрельбы информацию, что игра на паузе
            Cursor.visible = true; // Делаем курсор видимым
        }
        else
        {
            _pauseMenuUI.SetActive(false); 
            _playerStatsUI.SetActive(true); 
            _settingsMenuUI.SetActive(false); 
            Time.timeScale = 1f; // Ставим скорость времени в игре на 1
            _paused = false; // Убираем значение паузы
            _shootingLogic.SetTimeStopped(false); // Сообщаем скрипту стрельбы, что игра продолжилась
            _settingsOpened = false;
            Cursor.visible = false; // Скрываем курсор
        }
    }

    public void OpenSettings()
    {
        if (!_settingsOpened)
        {
            _pauseMenuUI.SetActive(false);
            _settingsMenuUI.SetActive(true);
            _settingsOpened = true;
            Cursor.visible = true;
        }
        else
        {
            _pauseMenuUI.SetActive(true);
            _settingsMenuUI.SetActive(false);
            _settingsOpened = false;
        }
    }

    public void ChangeCameraSensitivity()
    {
        // При смене показателя в плазунке чувствительности камеры меняем значение скорости у камеры, соответствующее плазунку, где макс. скорость = 600
        _virtualCamera.m_HorizontalAxis.m_MaxSpeed = 600 * _mouseSensitivitySlider.value;
        _virtualCamera.m_VerticalAxis.m_MaxSpeed = 600 * _mouseSensitivitySlider.value;
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
