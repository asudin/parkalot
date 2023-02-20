using UnityEngine;
using PathCreation;
using System.Collections;

public class CarMover : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private EndOfPathInstruction _pathEnd;

    private Rigidbody _rigidbody;
    private bool _hasEnteredTrigger = false;
    private bool _isMoving = false;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _direction;
    private float _speed = 20;
    private float _pathSpeed = 2;
    private float _distanceTraveled;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
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
        if (other.gameObject.TryGetComponent(out WinningZoneTrigger winzoneTrigger))
        {
            _hasEnteredTrigger = false;
            _rigidbody.isKinematic = false;
            Destroy(gameObject);
        }
        
        if (other.gameObject.TryGetComponent(out NavMeshPathTrigger pathTrigger))
        {
            _isMoving = false;
            StartCoroutine(MoveOnPath());
        }
    }

    private IEnumerator MoveOnPath()
    {
        Vector3 closestPoint = _pathCreator.path.GetClosestPointOnPath(transform.position);
        float distanceFromStart = _pathCreator.path.GetClosestDistanceAlongPath(closestPoint);

        while (Vector3.Distance(transform.position, closestPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, closestPoint, _speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(_pathCreator.path.GetDirectionAtDistance(distanceFromStart));

            closestPoint = _pathCreator.path.GetClosestPointOnPath(transform.position);
            distanceFromStart = _pathCreator.path.GetClosestDistanceAlongPath(closestPoint);
            yield return null;
        }

        _distanceTraveled = distanceFromStart;
        _hasEnteredTrigger = true;
        _rigidbody.isKinematic = true;

        while (_distanceTraveled < _pathCreator.path.length)
        {
            _distanceTraveled += _pathSpeed * Time.deltaTime;
            transform.position = _pathCreator.path.GetPointAtDistance(_distanceTraveled, _pathEnd);
            transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTraveled, _pathEnd);
            yield return null;
        }
    }
}
