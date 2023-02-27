using System.Collections.Generic;
using UnityEngine;

public class CarsContainer : MonoBehaviour
{
    public List<Car> Cars { get; private set; }

    private void Awake()
    {
        Cars = new List<Car>();
        Cars.AddRange(GetComponentsInChildren<Car>());
    }
}
