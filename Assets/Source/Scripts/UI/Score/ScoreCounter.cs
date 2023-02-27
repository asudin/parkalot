using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsScore;
    [SerializeField] private CarsContainer _carsContainer;
    
    private LevelCompletedScreen _levelCompletedScreen;
    private static int _playerCoins = 0;

    public int PlayerCoins => _playerCoins;

    private void OnEnable()
    {
        foreach (Car car in _carsContainer.Cars)
        {
            car.CollisionHandler.Collected += ChangeCoinsNumber;
        }
    }

    private void OnDisable()
    {
        foreach (Car car in _carsContainer.Cars)
        {
            car.CollisionHandler.Collected -= ChangeCoinsNumber;
        }
    }

    private void Awake()
    {
        _levelCompletedScreen = transform.parent.GetComponent<LevelCompletedScreen>();
    }

    private void Update()
    {
        if (_levelCompletedScreen.IsCanvasShown)
        {
            StartCoroutine(AnimateScoreCounter(_playerCoins, 2f));
        }
    }

    private void ChangeCoinsNumber(Car car)
    {
        _playerCoins += car.CarScore;
    }

    IEnumerator AnimateScoreCounter(int targetScore, float duration)
    {
        float elapsedTime = 0f;
        int startScore = 0;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            int currentScore = Mathf.RoundToInt(Mathf.Lerp(startScore, targetScore, progress));
            _coinsScore.text = currentScore.ToString();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _coinsScore.text = targetScore.ToString();
    }
}
