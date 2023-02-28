using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Level Selection"), Space]
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private int _level;
    [SerializeField] private ConfirmPanel _confirmPanel;

    [Header("Button Status"), Space]
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _lockedSprite;
    private Button _levelButton;
    private Image _buttonImage;
    private bool _isActive = true;

    [Header("Level Completion Status"), Space]
    [SerializeField] private Image[] _stars;

    private void Start()
    {
        _buttonImage = GetComponent<Image>();
        _levelButton = GetComponent<Button>();

        ShowCorrectLevel(_level);
        DecideSpriteStatus();
        ActivateStars();
    }

    private void DecideSpriteStatus()
    {
        if (_isActive)
        {
            _buttonImage.sprite = _activeSprite;
            _levelButton.enabled = true;
            _levelText.enabled = true;
        }else
        {

            _buttonImage.sprite = _lockedSprite;
            _levelButton.enabled = false;
            _levelText.enabled = false;
        }
    }

    private string ShowCorrectLevel(int currentLevel)
    {
        return _levelText.text = "" + currentLevel;
    }

    private void ActivateStars()
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].enabled = false;
        }
    }

    public void ShowConfirmPanel()
    {
        _confirmPanel.LevelToLoad = _level;
        _confirmPanel.gameObject.SetActive(true);
    }
}
