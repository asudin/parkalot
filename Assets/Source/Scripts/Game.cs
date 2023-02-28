using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private WinningZoneTrigger _winningZone;

    [Header("UI Screens")]
    [SerializeField] private LevelCompletedScreen _levelCompletedScreen;
    [SerializeField] private PauseGameScreen _pauseGameScreen;
    [SerializeField] private GameButtonScreen _gameButtonScreen;

    private void OnEnable()
    {
        _winningZone.LevelPassed += OnLevelCompleted;
        _levelCompletedScreen.HomeButtonClick += OnHomeButtonClick;
        _pauseGameScreen.UnpauseButtonClick += OnUnpauseButtonClick;
        _gameButtonScreen.HomeButtonClicked += OnHomeButtonClick;
        _gameButtonScreen.PauseButtonClicked += OnPauseButtonClick;
    }

    private void OnDisable()
    {
        _winningZone.LevelPassed -= OnLevelCompleted;
        _levelCompletedScreen.HomeButtonClick -= OnHomeButtonClick;
        _pauseGameScreen.UnpauseButtonClick -= OnUnpauseButtonClick;
        _gameButtonScreen.HomeButtonClicked -= OnHomeButtonClick;
        _gameButtonScreen.PauseButtonClicked -= OnPauseButtonClick;
    }

    private void OnLevelCompleted()
    {
        StartCoroutine(WaitForUnpause());
    }

    private void OnHomeButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    private void OnPauseButtonClick()
    {
        _pauseGameScreen.Open();
    }

    private void OnUnpauseButtonClick()
    {
        _pauseGameScreen.Close();
    }

    private IEnumerator WaitForUnpause()
    {
        while (_pauseGameScreen.IsShown)
        {
            yield return null;
        }

        _levelCompletedScreen.Open();
    }
}
