using UnityEngine;

public class LevelSelectScreen : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    public CanvasGroup CanvasGroup => _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        CanvasGroup.InstantOpen();
    }
}