using UnityEngine;

[RequireComponent(typeof(CarCollisionHandler))]
public class Car : MonoBehaviour
{
    [SerializeField] private CarCollisionHandler _collisionHandler;

    private static int _carScore = 10;

    public int CarScore => _carScore;
    public CarCollisionHandler CollisionHandler => _collisionHandler;

    private void Awake()
    {
        _collisionHandler = GetComponent<CarCollisionHandler>();
    }
}
