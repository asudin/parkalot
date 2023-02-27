using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WinningZoneTrigger : MonoBehaviour 
{
    [SerializeField] private List<Car> _collectableCars;

    private static int _collectedCars = 0;

    public int CollectedCars => _collectedCars;
    public event Action LevelPassed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Car car))
        {
            _collectedCars++;

            if (_collectedCars == _collectableCars.Count)
            {
                LevelPassed?.Invoke();
            }
        }
    }
}
