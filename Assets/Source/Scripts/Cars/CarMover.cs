using UnityEngine;
using PathCreation;
using System.Collections;
using DG.Tweening;

public class CarMover : MonoBehaviour
{
    [Header("Moving Path")]
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private EndOfPathInstruction _pathEnd;
    [SerializeField] private PauseGameScreen _pauseGameScreen;

    private CarCollisionHandler _collisionHandler;
    private Car _car;
    private bool _isMoving;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _direction;
    private Vector3 _frozenPosition;
    private float _speed = 20;
    private float _pathSpeed = 5;
    private float _distanceTraveled;

    public bool IsMoving
    {
        get => _isMoving;
        set => _isMoving = value;
    }

    private void Start()
    {
        _car = GetComponent<Car>();
        _collisionHandler = GetComponent<CarCollisionHandler>();
    }

    private void FixedUpdate()
    {
        if (IsMoving && !_pauseGameScreen.IsShown)
        {
            _car.Rigidbody.MovePosition(transform.position + _direction * _speed * Time.fixedDeltaTime);
        }
    }

    private void OnMouseDown()
    {
        if (!_collisionHandler.HasEnteredTrigger && !_pauseGameScreen.IsShown)
        {
            _startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        }
    }

    private void OnMouseUp()
    {
        if (!IsMoving && !_pauseGameScreen.IsShown)
        {
            _endPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            _direction = (_endPosition - _startPosition).normalized;
            _isMoving = true;
            _car.Animator.enabled = false;
        }
    }

    private IEnumerator MoveOnPath()
    {
        while (_distanceTraveled < _pathCreator.path.length)
        {
            if (_pauseGameScreen.IsShown)
            {
                _frozenPosition = transform.position;
                _isMoving = false;
                yield return new WaitWhile(() => _pauseGameScreen.IsShown);
                _isMoving = true;
                transform.position = _frozenPosition;
            }

            MoveCar();
            yield return null;
        }
    }

    private void MoveCar()
    {
        transform.position = _pathCreator.path.GetPointAtDistance(_distanceTraveled, _pathEnd);
        transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTraveled, _pathEnd);
        _distanceTraveled += _pathSpeed * Time.deltaTime;
    }

    public void EnterOnPath()
    {
        Vector3 closestPoint = _pathCreator.path.GetClosestPointOnPath(transform.position);
        float distanceFromStart = _pathCreator.path.GetClosestDistanceAlongPath(closestPoint);
        var direction = _pathCreator.path.GetRotationAtDistance(distanceFromStart, _pathEnd);
        _distanceTraveled = distanceFromStart;

        transform.DOMove(closestPoint, 0.3f)
            .OnComplete(() => transform.DORotate(direction.eulerAngles, 0.3f)
                .OnComplete(() => StartCoroutine(MoveOnPath())));
    }

    public void MoveCrashedCar(Vector3 movingDiretion)
    {
        float _crashMovePosition = 0.5f;
        _car.Rigidbody.MovePosition(transform.position + movingDiretion * _crashMovePosition);
    }

    public void StopCar()
    {
        _isMoving = false;
        _car.Rigidbody.velocity = Vector3.zero;
    }
}