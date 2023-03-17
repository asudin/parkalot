using System;
using UnityEngine;

public class CarCollisionHandler : MonoBehaviour
{
    private Car _car;
    private CarMover _mover;
    private Rigidbody _rigidbody;
    private bool _hasEnteredTrigger { get; set; }

    public event Action<Car> Collected;
    public bool HasEnteredTrigger => _hasEnteredTrigger;

    private void Awake()
    {
        _car = GetComponent<Car>();
        _mover = GetComponent<CarMover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out WinningZoneTrigger zone))
        {
            Collected?.Invoke(_car);
            _hasEnteredTrigger = false;
            _car.Rigidbody.isKinematic = false;
            Destroy(gameObject);
        }

        if (other.gameObject.TryGetComponent(out NavMeshPathTrigger pathTrigger))
        {
            _mover.IsMoving = false;
            _hasEnteredTrigger = true;
            _car.Rigidbody.isKinematic = true;

            _mover.EnterOnPath();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _mover.IsMoving = false;
            _car.Rigidbody.velocity = Vector3.zero;
            //_animator.SetTrigger("crashTrigger");
        }
    }
}