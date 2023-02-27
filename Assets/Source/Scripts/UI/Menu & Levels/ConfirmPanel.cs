using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmPanel : MonoBehaviour
{
    [SerializeField] private string _levelToLoad;

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(_levelToLoad);
    }
}
