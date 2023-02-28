using System;
using UnityEngine;
using UnityEngine.UI;

public class GameButtonScreen : MonoBehaviour
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _pauseButton;

    public event Action HomeButtonClicked;
    public event Action PauseButtonClicked;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(OnButtonClick);
        _pauseButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(OnButtonClick);
        _pauseButton.onClick.RemoveListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        HomeButtonClicked?.Invoke();
        PauseButtonClicked?.Invoke();
    }
}
