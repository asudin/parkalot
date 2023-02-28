using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private WinningZoneTrigger _winningZone;

    [Header("UI Screens")]
    [SerializeField] private LevelCompletedScreen _levelCompletedScreen;
    [SerializeField] private GameButtonScreen _gameButtonScreen;

    private void OnEnable()
    {
        _winningZone.LevelPassed += OnLevelCompleted;
        _gameButtonScreen.HomeButtonClicked += OnHomeButtonClick;
    }

    private void OnDisable()
    {
        _winningZone.LevelPassed -= OnLevelCompleted;
        _gameButtonScreen.HomeButtonClicked -= OnHomeButtonClick;
    }

    public void OnLevelCompleted()
    {
        _levelCompletedScreen.Open();
    }

    private void OnHomeButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
