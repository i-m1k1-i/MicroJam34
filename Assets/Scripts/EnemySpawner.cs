using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _leftMin, _leftMax, _rightMin, _rightMax;
    [SerializeField] private Transform _enemysTarget;
    [SerializeField] private GameController _gameController;

    private bool _isSpawning;

    private int _enemyAmount;
    private float _spawnDelay;
    private float _spawnDelayCurrent;


    private void Update()
    {
        if (_gameController._pause)
        {
            return;
        }

        if (_isSpawning == false)
        {
            return;
        }
        if (_spawnDelayCurrent > 0)
        {
            _spawnDelayCurrent -= Time.deltaTime;
            return;
        }

        Spawn();

        if (_enemyAmount == 0)
        {
            _isSpawning = false;
        }
    }

    public void SetNewWave(int enemyAmount, float spawnDelay)
    {
        _isSpawning = true;
        _enemyAmount = enemyAmount;
        _spawnDelay = spawnDelay;
    }

    private void Spawn()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = GetRandomPosition();
        enemy.SetTarget(_enemysTarget);

        _enemyAmount--;
        _spawnDelayCurrent = _spawnDelay;
    }

    private Vector2 GetRandomPosition()
    {
        int randomSide = Random.Range(1, 100) % 2;
        Debug.Log(randomSide);
        Transform min = randomSide == 0 ? _leftMin : _rightMin;
        Transform max = randomSide == 0 ? _leftMax : _rightMax;

        float randomX = Random.Range(min.position.x, max.position.x);
        float randomY = Random.Range(min.position.y, max.position.y);

        return new Vector2(randomX, randomY);
    }
}
