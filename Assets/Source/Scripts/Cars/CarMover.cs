using UnityEngine;
using UnityEngine.AI;

public class CarMover : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;
    private NavMeshAgent _navmeshagent;
    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _direction;
    private bool _isMoving = false;
    private bool _hasEnteredTrigger = false;
    private float _speed = 20;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _navmeshagent = GetComponent<NavMeshAgent>();
        _navmeshagent.updatePosition = false;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _rigidbody.MovePosition(transform.position + _direction * _speed * Time.fixedDeltaTime);
        }
        
        if (_hasEnteredTrigger)
        {
            _navmeshagent.enabled = true;
            _navmeshagent.updatePosition = true;
            _navmeshagent.destination = _targetPosition.position;
        }
    }

    private void OnMouseDown()
    {
        if (!_isMoving)
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
        if (other.TryGetComponent(out Road road))
        {
            _hasEnteredTrigger = true;
        }
    }
}
