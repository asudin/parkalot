using System;
using Random = UnityEngine.Random;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CarCollisionHandler : MonoBehaviour
{
    [SerializeField] public Sprite[] _crashEmoji;

    private Car _car;
    private Animator _animator;

    public event Action<Car> Collected;

    private void Awake()
    {
        _car = GetComponent<Car>();
        _animator = GetComponent<Animator>();
    }

    public void Crash(Sprite[] crashEmoji)
    {
        int randomEmoji = Random.Range(0, crashEmoji.Length);
        Vector3 spawnPosition = transform.position + new Vector3(0, 10, 0);

        Instantiate(crashEmoji[randomEmoji], spawnPosition, Quaternion.identity);
        Debug.Log("spawn emoji");
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
