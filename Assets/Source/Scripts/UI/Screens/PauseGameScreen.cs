using System;
using UnityEngine;

public class PauseGameScreen : Screen
{
    private bool _isShown = false;

    public bool IsShown => _isShown;

    public event Action UnpauseButtonClick;

    public override void Close()
    {
        _isShown = false;
        CanvasGroup.InstantClose();
    }

    public override void Open()
    {
        _isShown = true;
        CanvasGroup.InstantOpen();
    }

    protected override void OnButtonClick()
    {
        UnpauseButtonClick?.Invoke();
    }
}
