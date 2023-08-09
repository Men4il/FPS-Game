using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float _damage = 15f; // Урон оружия
    private float _fireRate = .25f; // Скорость стрельбы
    private int _maxBullets = 30; // Макс. кол-во патронов
    private float _range = 50f; // Дальность стрельбы
    private float _reloadingSpeed = .9f; // Скорость перезарядки

    // Функции для доступа к переменным игрока из других скриптов
    public float GetFireRate()
    {
        return _fireRate;
    }

    public float GetWeaponRange()
    {
        return _range;
    }

    public float GetGunDamage()
    {
        return _damage;
    }

    public int GetMaxBullets()
    {
        return _maxBullets;
    }

    public float GetReloadSpeed()
    {
        return _reloadingSpeed;
    }
}
