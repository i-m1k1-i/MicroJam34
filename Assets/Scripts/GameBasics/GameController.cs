using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Wave[] _waves;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private float _overallTime = 3 * 45;
    [SerializeField] private float _loseTime = 230;


    private int _currentWaveIndex = 0;
    private Wave _currentWave;
    private int _killsInWave;
    private float _waveTime;
    public bool _pause;

    public float OverallTime => _overallTime;

    public event UnityAction<float> TimePassed;
    public event UnityAction<int> WaveCleared;
    public event UnityAction DoubleShotWaveReached;
    public event UnityAction<bool> GameEnd;

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
        _currentWaveIndex++;

        if (CheckGameEnd())
        {
            return;
        }
        
        if (_currentWaveIndex == 4)
        {
            DoubleShotWaveReached?.Invoke();
        }

        WaveCleared?.Invoke(_currentWaveIndex);
        _currentWave = _waves[_currentWaveIndex];
        _waveTime = _currentWave.WaveTime;
        _spawner.SetNewWave(_currentWave.EnemyAmount, _currentWave.SpawnTime);
        _killsInWave = 0;
    }

    private bool CheckGameEnd()
    {
        if (_overallTime <= 0)
        {
            GameEnd?.Invoke(true);
            StopGame();
            return true;
        }
        if (_overallTime >= _loseTime || _currentWaveIndex >= _waves.Length)
        {
            GameEnd?.Invoke(false);
            StopGame();
            return true;
        }

        return false;
    }

    private void StopGame()
    {
        _pause = true;
        _player.enabled = false;
    }

    private void HandleKill()
    {
        _killsInWave++;
        if (_killsInWave == _currentWave.EnemyAmount)
        {
            NextWave();
        }
    }

    private void HandleTargetReaching(float damageTime)
    {
        _waveTime -= damageTime;
        TimePassed?.Invoke(_waveTime);
    }

    private void OnEnable()
    {
        EnemyHealth.Dead += HandleKill;
        Enemy.TargetReached += HandleTargetReaching;
    }

    private void OnDisable()
    {
        EnemyHealth.Dead -= HandleKill;
        Enemy.TargetReached -= HandleTargetReaching;
    }
}
