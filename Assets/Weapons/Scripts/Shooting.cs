using System.Collections;
using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private PlayerControlsInitializer _playerInputs;
    [SerializeField] private TextMeshProUGUI _bulletsCounter;

    public Transform _gunEnd;
    private Camera _fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private LineRenderer _laserLine;
    private Weapon _currentWeapon;
    private Animator _animator;
    private float _nextFire;
    private float _currentBullets;
    private bool _reloading;
    private bool _timeStopped;

    private void Start()
    {
        // Присваиваем переменным соответственные объекты
        _currentWeapon = GetComponent<Weapon>();
        _laserLine = GetComponent<LineRenderer>();
        _fpsCam = Camera.main;
        _currentBullets = _currentWeapon.GetMaxBullets();
        _animator = GetComponent<Animator>();
        
        // Обновляем счётчик патронов на интерфейсе
        UpdateBulletCounter();
    }
    
    void Update ()
    {
        Shoot();
        if (_playerInputs.ReloadInput && _currentBullets != _currentWeapon.GetMaxBullets() && !_reloading)
        {
            // Если игрок нажал "R", то начинаем корутину с интерфейсом Reload
            StartCoroutine(Reload());
            return;
        }
    }

    private void Shoot()
    {
        if (_playerInputs.ShootInput && Time.time > _nextFire && _currentBullets > 0 && !_reloading && !_timeStopped)
        {
            _currentBullets--; // Отнимаем кол-во патронов
            _nextFire = Time.time + _currentWeapon.GetFireRate(); // Ждём время перед следующим выстрелом
            StartCoroutine (ShotEffect());
            Vector3 rayOrigin = _fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            _laserLine.SetPosition (0, _gunEnd.position); // Задаём начальную точку луча в дуле пистолета

            if (Physics.Raycast (rayOrigin, _fpsCam.transform.forward, out hit, _currentWeapon.GetWeaponRange())) // Строим луч от дула пистолета до точки зрения игрока и проверяем, попал ли он в колайдер
            {
                _laserLine.SetPosition (1, hit.point);
                Enemy enemy = hit.collider.GetComponentInParent<Enemy>(); // Берём с элемента с колайдером, в который попал луч, компонент Enemy

                if (enemy != null) // Проверяем, есть ли на объекте компонент Enemy
                {
                    enemy.Damage (_currentWeapon.GetGunDamage()); // Вызываем метод, наносящий урон врагу
                }
            }
            else
            {
                _laserLine.SetPosition (1, rayOrigin + (_fpsCam.transform.forward * _currentWeapon.GetWeaponRange())); //Если луч не попал в колайдер, то строим луч от дула пистолета, до точки зрения игрока длиною в Weapon.Range
            }

            UpdateBulletCounter(); // Обновляем счётчик патронов
        }
    }

    private IEnumerator Reload()
    {
        _reloading = true; // Ставим нынешнее состояние в перезарядку
        _animator.SetBool("reloading", true); // Сообщаем аниматору, что перезарядку началась
        yield return new WaitForSeconds(_currentWeapon.GetReloadSpeed()); // Ждём время перезарядки для проигрывания анимации
        _animator.SetBool("reloading", false);
        _currentBullets = _currentWeapon.GetMaxBullets(); // Задаём кол-во патронов в максимум
        _reloading = false;
        
        UpdateBulletCounter();
    }

    private void UpdateBulletCounter()
    {
        _bulletsCounter.text = $"{_currentBullets}/{_currentWeapon.GetMaxBullets()}";
    }
    
    private IEnumerator ShotEffect()
    {
        _laserLine.enabled = true;
        yield return shotDuration;
        _laserLine.enabled = false;
    }

    public void SetTimeStopped(bool timeStop)
    {
        _timeStopped = timeStop;
    }
}
