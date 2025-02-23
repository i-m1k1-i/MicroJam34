using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shootPoint;

    private bool _doubleShot = false;

    public void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.Set(_shootPoint.position, _shootPoint.position - transform.position);

        if (_doubleShot)
        {
            StartCoroutine(DoSecondShot());
        }
    }

    private IEnumerator DoSecondShot()
    {
        yield return new WaitForSeconds(0.1f);
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.Set(_shootPoint.position, _shootPoint.position - transform.position);
    }

    private void HandleWaveClear(int waveIndex)
    {
        if (waveIndex + 1 == 5)
        {
            _doubleShot = true;
        }
    }

    private void OnEnable()
    {
        _gameController.WaveCleared += HandleWaveClear;
    }

    private void OnDisable()
    {
        _gameController.WaveCleared -= HandleWaveClear;
    }
}
