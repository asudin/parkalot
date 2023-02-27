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
    private float _pathSpeed = 5;
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
            _hasEnteredTrigger = true;
            _rigidbody.isKinematic = true;
            StartCoroutine(MoveOnPath());
        }
    }

    private IEnumerator MoveOnPath()
    {
        Vector3 closestPoint = _pathCreator.path.GetClosestPointOnPath(transform.position);
        float distanceFromStart = _pathCreator.path.GetClosestDistanceAlongPath(closestPoint);
        _distanceTraveled = distanceFromStart;

        //while (transform.position != closestPoint)
        //{
        //    RotateCar(closestPoint, distanceFromStart);
        //    yield return null;
        //}

        while (_distanceTraveled < _pathCreator.path.length)
        {
            MoveCar();
            yield return null;
        }
    }

    private void RotateCar(Vector3 closestPoint, float distanceFromStart)
    {
        transform.position = Vector3.MoveTowards(transform.position, closestPoint, _pathSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(_pathCreator.path.GetDirectionAtDistance(distanceFromStart));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _pathSpeed * Time.deltaTime);
    }

    private void MoveCar()
    {
        transform.position = _pathCreator.path.GetPointAtDistance(_distanceTraveled, _pathEnd);
        transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTraveled, _pathEnd);
        _distanceTraveled += _pathSpeed * Time.deltaTime;
    }
}
