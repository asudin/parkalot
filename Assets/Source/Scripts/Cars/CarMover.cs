using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMover : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private Transform _endTrigger;

    private NavMeshAgent _navmeshagent;
    private Rigidbody _rigidbody;
    private bool _hasEnteredTrigger = false;
    private bool _isMoving = false;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _direction;
    private float _speed = 20;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _navmeshagent = GetComponent<NavMeshAgent>();
        _navmeshagent.updatePosition = false;
    }

    private void FixedUpdate()
    {
        if (_hasEnteredTrigger)
        {
            _navmeshagent.updatePosition = true;
            _navmeshagent.SetDestination(_targetPosition.position);
        }

        if (_isMoving)
        {
            _rigidbody.MovePosition(transform.position + _direction * _speed * Time.fixedDeltaTime);
        }
    }

    private void OnMouseDown()
    {
        if (!_hasEnteredTrigger)
        {
            _startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        }
    }

    private void OnMouseUp()
    {
        if (!_isMoving)
        {
            _endPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            _direction = (_endPosition - _startPosition).normalized;
            _isMoving = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _endTrigger)
        {
            _hasEnteredTrigger = false;
            _rigidbody.isKinematic = false;
            _navmeshagent.enabled = false;
            Destroy(gameObject);
        }
        else if (other.gameObject.TryGetComponent(out NavMeshPathTrigger trigger))
        {
            _hasEnteredTrigger = true;
            _rigidbody.isKinematic = true;
            _navmeshagent.enabled = true;
        }
    }
}
