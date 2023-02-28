using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButtonScreen : MonoBehaviour
{
    [SerializeField] private Button _homeButton;

    public event Action HomeButtonClicked;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        HomeButtonClicked?.Invoke();
    }
}
