using UnityEngine;

public class CarMover : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _direction;
    private bool _isMoving = false;
    private float _speed = 20;

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
}
