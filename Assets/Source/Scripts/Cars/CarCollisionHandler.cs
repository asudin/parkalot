using System;
using Random = UnityEngine.Random;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CarCollisionHandler : MonoBehaviour
{
    [SerializeField] private Sprite[] _crashEmoji;

    private Car _car;
    private Animator _animator;

    public event Action<Car> Collected;

    private void Awake()
    {
        _car = GetComponent<Car>();
        _animator = GetComponent<Animator>();
    }

    private void Crash(Sprite[] crashEmoji)
    {
        int randomEmoji = Random.Range(0, crashEmoji.Length);
        Instantiate(crashEmoji[randomEmoji], transform.position, Quaternion.identity);
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
            Crash(_crashEmoji);
            _animator.Play("Crash");
        }
    }
}
