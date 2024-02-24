using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainObstacle : MonoBehaviour
{
    // Tren para ile kar��la�t���nda paralar� yukar� ��kar�r ve engelleri yok eder.
    [SerializeField] private float coinHeight = 4.0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            other.gameObject.transform.position = new Vector3(other.transform.position.x, coinHeight, other.transform.position.z);
        }

        if (other.CompareTag("Obstacle"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
