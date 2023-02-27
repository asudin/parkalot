using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Image[] _stars;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private int _level;
    [SerializeField] private ConfirmPanel _confirmPanel;

    public void ConfirmPanel()
    {
        _confirmPanel.gameObject.SetActive(true);
    }
}
