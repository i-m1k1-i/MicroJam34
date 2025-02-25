using TMPro;
using UnityEngine;

public class WaveInfoView : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private TextMeshProUGUI _timeTMP;
    [SerializeField] private TextMeshProUGUI _waveTMP;
    [SerializeField] private TextMeshProUGUI _overallTimeTMP;

    private void SetTimeText(float time)
    {
        _timeTMP.text = ((int)time).ToString();
    }

    private void HandleWaveClear(int waveIndex)
    {
        _waveTMP.text = (++waveIndex).ToString();
        _overallTimeTMP.text = ((int)_gameController.OverallTime).ToString();
    }

    private void OnEnable()
    {
        _gameController.TimePassed += SetTimeText;
        _gameController.WaveCleared += HandleWaveClear;
    }

    private void OnDisable()
    {
        _gameController.TimePassed -= SetTimeText;
        _gameController.WaveCleared -= HandleWaveClear;
    }
}
