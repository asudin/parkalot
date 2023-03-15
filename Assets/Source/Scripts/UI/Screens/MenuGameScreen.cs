using System;
using UnityEngine;

public class MenuGameScreen : Screen
{
    public event Action PlayButtonClick;

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
        PlayButtonClick?.Invoke();
    }
}
