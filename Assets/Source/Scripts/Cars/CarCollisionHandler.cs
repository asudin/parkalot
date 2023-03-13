using System;
using Random = UnityEngine.Random;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CarCollisionHandler : MonoBehaviour
{
    private Car _car;
    private Animator _animator;

    public event Action<Car> Collected;

    private void Awake()
    {
        _car = GetComponent<Car>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out WinningZoneTrigger zone))
        {
            Collected?.Invoke(_car);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Car car))
        {
            _animator.Play("Crash");
        }
    }
}
