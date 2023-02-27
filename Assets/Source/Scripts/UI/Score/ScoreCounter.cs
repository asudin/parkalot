using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsScore;
    [SerializeField] private List<Car> _cars;

    private static int _playerCoins = 0;

    public int PlayerCoins => _playerCoins;

    private void OnEnable()
    {
        foreach (Car car in _cars)
        {
            car.CollisionHandler.Collected += ChangeCoinsNumber;
        }
    }

    private void OnDisable()
    {
        foreach (Car car in _cars)
        {
            car.CollisionHandler.Collected -= ChangeCoinsNumber;
        }
    }

    private void Update()
    {
        _coinsScore.text = _playerCoins.ToString();
    }

    private void ChangeCoinsNumber(Car car)
    {
        _playerCoins += car.CarScore;
    }
}
