using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmPanel : MonoBehaviour
{
    [SerializeField] private Image[] _stars;

    public int LevelToLoad { get; set; }

    private void Start()
    {
        ActivateStars();
    }

    private void ActivateStars()
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].enabled = false;
        }
    }

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }

    public void Play()
    {
        PlayerPrefs.SetInt("Current Level", LevelToLoad - 1);
        SceneManager.LoadScene(LevelToLoad);
    }
}
