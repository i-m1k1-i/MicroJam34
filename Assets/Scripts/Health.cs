using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public static event UnityAction Dead;
    public event UnityAction<int> DamageTaken;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        DamageTaken?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Dead?.Invoke();
        AudioManager.Instance.PlayEnemyDeath();
        Destroy(gameObject);
    }
}
