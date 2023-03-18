using System;
using UnityEngine;

public class PauseGameScreen : Screen
{
    [SerializeField] private GameButtonScreen _gameButtonScreen;
    
    private bool _isShown = false;

    public bool IsShown => _isShown;

    public event Action UnpauseButtonClick;

    public override void Close()
    {
        _isShown = false;
        CanvasGroup.InstantClose();
        _gameButtonScreen.OpenCanvas();
    }

    public override void Open()
    {
        _isShown = true;
        CanvasGroup.InstantOpen();
        _gameButtonScreen.CloseCanvas();
    }

    protected override void OnButtonClick()
    {
        UnpauseButtonClick?.Invoke();
    }
}
