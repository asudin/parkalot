using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("UI Screens")]
    [SerializeField] private MenuGameScreen _menuGameScreen;
    [SerializeField] private LevelSelectScreen _levelSelectScreen;

    private void OnEnable()
    {
        _menuGameScreen.PlayButtonClick += OnPlayButtonClick;
    }

    private void OnDisable()
    {
        _menuGameScreen.PlayButtonClick -= OnPlayButtonClick;
    }

    private void OnPlayButtonClick()
    {
        _menuGameScreen.Close();
        _levelSelectScreen.Open();
    }
}
