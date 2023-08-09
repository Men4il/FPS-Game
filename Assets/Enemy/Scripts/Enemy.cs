using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image _healthBarFill;
    
    private float _currentHealth = 100; // Здоровье врага
    private float _maxHealth = 100; // Максимально возможное здоровье врага

    public void Damage(float damageAmount) // Нанести врагу урон
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0) 
        {
            // Если хп врага < или = 0, то делаем его инактивным
            gameObject.SetActive (false);
        }

        _healthBarFill.fillAmount = _currentHealth / _maxHealth; // Обновляем кол-во хп у врага в ХП баре
    }
}
