using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shootPoint;

    public void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.Set(_shootPoint.position, _shootPoint.position - transform.position);
    }
}
