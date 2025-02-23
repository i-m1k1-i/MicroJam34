using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Wave[] _waves;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Canvas _doubleShotCanvas;
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private Canvas _loseCanvas;
    [SerializeField] private float _overallTime = 3 * 45;

    private int _currentWaveIndex = 0;
    private Wave _currentWave;
    private int _killedEnemies;
    private float _waveTime;
    public bool _pause;

    public float OverallTime => _overallTime;

    public event UnityAction<float> TimePassed;
    public event UnityAction<int> WaveCleared;

    private void Start()
    {
        _currentWave = _waves[_currentWaveIndex];
        _waveTime = _currentWave.WaveTime;
        _spawner.SetNewWave(_currentWave.EnemyAmount, _currentWave.SpawnTime);
    }

    private void Update()
    {
        _waveTime -= Time.deltaTime;
        TimePassed?.Invoke(_waveTime);
    }

    private void NextWave()
    {
        if (_pause)
        {
            return;
        }
        _overallTime = Mathf.Clamp(_overallTime - _waveTime, 0, float.MaxValue);
        if (_overallTime <= 0)
        {
            _winCanvas.gameObject.SetActive(true);
            Debug.Log("Win");
            StopGame();
        }

        if (_overallTime >= 230)
        {
            _loseCanvas.gameObject.SetActive(true);
            Debug.Log("Lose");
            StopGame();
        }
        Debug.Log("Wave cleared");
        Debug.Log(_overallTime);

        _currentWaveIndex++;
        _currentWave = _waves[_currentWaveIndex];
        _waveTime = _currentWave.WaveTime;
        _spawner.SetNewWave(_currentWave.EnemyAmount, _currentWave.SpawnTime);
        _killedEnemies = 0;

        WaveCleared?.Invoke(_currentWaveIndex);

        if (_currentWaveIndex + 1 == 5)
        {
            _doubleShotCanvas.gameObject.SetActive(true);
        }
        if (_currentWaveIndex >= _waves.Length)
        {
            _loseCanvas.gameObject.SetActive(true);
            StopGame();
        }
    }

    private void StopGame()
    {
        _pause = true;
        _player.enabled = false;
    }

    private void HandleKill()
    {
        _killedEnemies++;
        if (_killedEnemies == _currentWave.EnemyAmount)
        {
            NextWave();
        }
    }

    private void HandlePortalReaching()
    {
        _waveTime -= 10;
        TimePassed?.Invoke(_waveTime);
    }

    private void OnEnable()
    {
        Health.Dead += HandleKill;
        Enemy.ReachedPortal += HandlePortalReaching;
    }

    private void OnDisable()
    {
        Health.Dead -= HandleKill;
        Enemy.ReachedPortal -= HandlePortalReaching;
    }
}
