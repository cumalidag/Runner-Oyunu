using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    // Araba oyuncuya doðru hareket eder.
    [SerializeField] private float carSpeed = 10.0f;

    private void Update()
    {
       transform.Translate(Vector3.forward * Time.deltaTime * carSpeed);
    }
    
}
