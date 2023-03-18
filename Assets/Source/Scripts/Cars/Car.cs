using UnityEngine;

[RequireComponent(typeof(CarCollisionHandler))]
public class Car : MonoBehaviour
{
    [Header("Configurations")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Crash collision")]
    [SerializeField] private CarCollisionHandler _collisionHandler;
    [SerializeField] private Emoji _emoji;

    private static int _carScore = 10;

    public int CarScore => _carScore;
    public CarCollisionHandler CollisionHandler => _collisionHandler;
    public Animator Animator => _animator;
    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _collisionHandler = GetComponent<CarCollisionHandler>();
    }

    public void ShowEmoji()
    {
        _emoji.RandomEmoji();
    }
}