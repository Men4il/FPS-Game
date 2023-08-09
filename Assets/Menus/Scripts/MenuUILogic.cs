using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUILogic : MonoBehaviour
{
    [SerializeField] private float _rotatingSpeed;

    private Camera _camera;

    private void OnEnable()
    {
        // Делаем курсор видимым и задаём нужную камеру
        Cursor.visible = true;
        _camera = Camera.main;
    }

    void Update()
    {
        // Поворачиваем камеру каждый кадр, соответственно со скоростью поворота по оси y
        _camera.transform.Rotate(0, 1f * _rotatingSpeed * Time.deltaTime, 0, Space.World);
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
