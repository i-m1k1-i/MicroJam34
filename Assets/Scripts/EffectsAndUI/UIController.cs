using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private Canvas _loseCanvas;
    [SerializeField] private Canvas _doubleShotCanvas;

    private void HandleGameEnd(bool isWon)
    {
        if (isWon)
        {
            _winCanvas.gameObject.SetActive(true);
        }
        else
        {
            _loseCanvas.gameObject.SetActive(true);
        }
    }

    private void DoubleShotAlert()
    {
        _doubleShotCanvas.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _gameController.GameEnd += HandleGameEnd;
        _gameController.DoubleShotWaveReached += DoubleShotAlert;
    }

    private void OnDisable()
    {
        _gameController.GameEnd -= HandleGameEnd;
        _gameController.DoubleShotWaveReached -= DoubleShotAlert;
    }
}
