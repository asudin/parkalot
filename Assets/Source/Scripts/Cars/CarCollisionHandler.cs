using System;
using UnityEngine;

public class CarCollisionHandler : MonoBehaviour
{
    private Car _car;

    public event Action<Car> Collected;

    private void Awake()
    {
        _car = GetComponent<Car>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out WinningZoneTrigger zone))
        {
            Collected?.Invoke(_car);
        }
    }
}
