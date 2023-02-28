using System;
using UnityEngine;

public class LevelCompletedScreen : Screen
{
    [SerializeField, Tooltip("Higher positive value equals faster fade in/out animation.")] private float _fadeTime;

    private bool _isCanvasShown = false;

    public bool IsCanvasShown => _isCanvasShown;

    public event Action HomeButtonClick;

    public override void Close()
    {
        _isCanvasShown = false;
        CanvasGroup.InstantClose();
    }

    public override void Open()
    {
        _isCanvasShown = true;
        StartCoroutine(CanvasGroup.FadeIn(_fadeTime));
    }

    protected override void OnButtonClick()
    {
        HomeButtonClick?.Invoke();
    }
}
