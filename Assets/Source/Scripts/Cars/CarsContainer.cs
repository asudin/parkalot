using UnityEngine;

public class CarsContainer : MonoBehaviour
{
    public int TotalCarsCount { get; private set;}

    private void Awake()
    {
        TotalCarsCount = transform.childCount;
    }
}
