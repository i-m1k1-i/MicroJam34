using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Vector2 _direction;

    private void Update()
    {
        Vector3 moveVector = _speed * Time.deltaTime * _direction;
        transform.Translate(moveVector);
    }

    public void Set(Vector3 startPosition, Vector2 direction)
    {
        transform.position = startPosition;
        _direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyHealth>(out EnemyHealth health))
        {
            health.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
