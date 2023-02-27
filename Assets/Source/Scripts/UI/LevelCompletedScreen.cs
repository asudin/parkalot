using System;
using UnityEngine;

public class LevelCompletedScreen : Screen
{
    [SerializeField, Tooltip("Higher positive value equals faster fade in/out animation.")] private float _time;

    private bool _isCanvasShown = false;

    public event Action ShowCanvas;
    public bool IsCanvasShown => _isCanvasShown;

    public override void Close()
    {
        _isCanvasShown = false;
        CanvasGroup.InstantClose();
    }

    public override void Open()
    {
        _isCanvasShown = true;
        ShowCanvas?.Invoke();
        StartCoroutine(CanvasGroup.FadeIn(_time));
    }
}
