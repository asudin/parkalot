using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButtonScreen : MonoBehaviour
{
    [SerializeField] private Button _homeButton;

    public event Action HomeButtonClicked;
}
