using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Kendisine deðen objeleri object poola geri gönderir.
    [SerializeField] private Transform player;
    private Vector3 offset = Vector3.zero;

    
    private void Start()
    {
        offset = transform.position - player.position;
    }
    private void Update()
    {
      
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z + offset.z);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Train"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Car"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
