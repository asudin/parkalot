using System;
using UnityEngine;

public class PauseGameScreen : Screen
{
    public event Action UnpauseButtonClick;

    public override void Close()
    {
        CanvasGroup.InstantClose();
    }

    public override void Open()
    {
        CanvasGroup.InstantOpen();
    }

    protected override void OnButtonClick()
    {
        UnpauseButtonClick?.Invoke();
    }
}
