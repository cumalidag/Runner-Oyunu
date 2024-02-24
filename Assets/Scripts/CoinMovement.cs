using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    // Coin'in dönmesini saðlar.
    [SerializeField] private float coinRotateSpeed = 5f;
    private void Update()
    {
        transform.Rotate(Vector3.up, 100 * Time.deltaTime * coinRotateSpeed);
    }
}
