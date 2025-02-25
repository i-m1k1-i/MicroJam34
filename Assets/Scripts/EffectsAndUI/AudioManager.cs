using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _enemyDeath;
    [SerializeField] private AudioClip _playerShot;

    private AudioSource _audioSource;

    public static AudioManager Instance;

    private void Start()
    {
        if (Instance == null)
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayEnemyDeath()
    {
        _audioSource.PlayOneShot(_enemyDeath, 0.5f);
    }

    public void PlayPlayerShot()
    {
        _audioSource.PlayOneShot(_playerShot);
    }
}
