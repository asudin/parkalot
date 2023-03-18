using System;
using UnityEngine;
using UnityEngine.UI;

public class GameButtonScreen : MonoBehaviour
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _pauseButton;

    private CanvasGroup _canvasGroup;

    public event Action HomeButtonClicked;
    public event Action PauseButtonClicked;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(OnHomeButtonClick);
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(OnHomeButtonClick);
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnHomeButtonClick()
    {
        HomeButtonClicked?.Invoke();
    }

    private void OnPauseButtonClick()
    {
        PauseButtonClicked?.Invoke();
    }

    public void OpenCanvas()
    {
        _canvasGroup.InstantOpen();
    }

    public void CloseCanvas()
    {
        _canvasGroup.InstantClose();
    }
}
