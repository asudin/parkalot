using System;
using UnityEngine;

public class CarCollisionHandler : MonoBehaviour
{
    [Header("Colliders")]
    [SerializeField] private BoxCollider _frontCollider;
    [SerializeField] private BoxCollider _backCollider;

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
            _car.Animator.enabled = true;
            _car.Animator.SetTrigger("crashTrigger");
            _mover.StopCar();
            //CheckCollider(collision);
        }

        if (collision.collider.TryGetComponent(out Car car))
        {
            _mover.StopCar();
            CheckCollider(collision);
            car.Animator.SetTrigger("crashTrigger");
        }
    }

    private void CheckCollider(Collision collision)
    {
        Vector3 oppositeDirection = Vector3.zero;
        ContactPoint contact = collision.contacts[0];

        if (contact.thisCollider == _frontCollider)
        {
            oppositeDirection = -transform.forward;
            _mover.MoveCrashedCar(oppositeDirection);
        }
        else if (contact.thisCollider == _backCollider)
        {
            oppositeDirection = transform.forward;
            _mover.MoveCrashedCar(oppositeDirection);
        }
    }
}