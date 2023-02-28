using UnityEngine;

public class LevelCompletedScreen : Screen
{
    [SerializeField, Tooltip("Higher positive value equals faster fade in/out animation.")] private float _time;

    private bool _isCanvasShown = false;

    public bool IsCanvasShown => _isCanvasShown;

    public override void Close()
    {
        _isCanvasShown = false;
        CanvasGroup.InstantClose();
    }

    public override void Open()
    {
        _isCanvasShown = true;
        StartCoroutine(CanvasGroup.FadeIn(_time));
    }
}
