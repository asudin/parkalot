using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private WinningZoneTrigger _winningZone;

    [Header("UI Screens")]
    [SerializeField] private LevelCompletedScreen _levelCompletedScreen;

    private bool _isCompleted = false;

    private void OnEnable()
    {
        _winningZone.LevelPassed += OnGameOver;
        //_levelCompletedScreen.ShowCanvas += OnRestartGame;
    }

    private void OnDisable()
    {
        _winningZone.LevelPassed -= OnGameOver;
        //_levelCompletedScreen.ShowCanvas -= OnRestartGame;
    }
    private void Update()
    {
        if (_isCompleted)
        {
            RestartGame();
        }
    }

    private void OnRestartGame()
    {
        _isCompleted = true;
    }

    private void RestartGame()
    {
        //if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
    }

    public void OnGameOver()
    {
        _levelCompletedScreen.Open();
    }
}
