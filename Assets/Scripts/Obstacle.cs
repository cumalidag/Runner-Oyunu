using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Objeler oyuncuya doðru hareket eder.
    [SerializeField] private float obstacleSpeed = 10.0f;
    private void Update()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * obstacleSpeed);

    }
}
