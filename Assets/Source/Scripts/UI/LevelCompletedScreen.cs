using System;
using TMPro;
using UnityEngine;

public class LevelCompletedScreen : Screen
{
    [SerializeField, Tooltip("Higher positive value equals faster fade in/out animation.")] private float _time;

    public event Action ShowCanvas;

    public override void Close()
    {
        CanvasGroup.InstantClose();
    }

    public override void Open()
    {
        ShowCanvas?.Invoke();
        StartCoroutine(CanvasGroup.FadeIn(_time));
    }
}
