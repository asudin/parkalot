using UnityEngine;
using PathCreation;
using System.Collections;
using DG.Tweening;

public class CarMover : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator _animator;

    [Header("Moving Path")]
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private EndOfPathInstruction _pathEnd;
    [SerializeField] private PauseGameScreen _pauseGameScreen;

    private CarCollisionHandler _collisionHandler;
    private Rigidbody _rigidbody;
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
    public Rigidbody Rigidbody => _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collisionHandler = GetComponent<CarCollisionHandler>();
    }

    private void FixedUpdate()
    {
        if (IsMoving && !_pauseGameScreen.IsShown)
        {
            _rigidbody.MovePosition(transform.position + _direction * _speed * Time.fixedDeltaTime);
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
            _animator.enabled = false;
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
}