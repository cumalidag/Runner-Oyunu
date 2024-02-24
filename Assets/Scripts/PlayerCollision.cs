using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Oyuncunun çarpýþma durumlarýný kontrol eder.
    [SerializeField] private GameObject deathAnimationObjects;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float unTouchableTime = 0.5f;
    private int currentHealth = 3;
    [SerializeField] private Animator anim;
    private bool isDead = false;
    private bool unTouchable = false;

    public bool IsDead { get => isDead; set => isDead = value; }

    public event EventHandler OnPlayerDeath;
    public event EventHandler OnCoinCollide;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            anim.SetBool("Dead", true);
            isDead = true;
            playerMovement.PlayerSpeed = 0;
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Obstacle") && !unTouchable)
        {
            currentHealth--;
            if (currentHealth > 0)
            {
                SetActiveFalse(false);
                StartCoroutine(WaitAndColorChange(unTouchableTime));
            }
            collider.gameObject.SetActive(false);
        }

        if (collider.gameObject.CompareTag("Coin"))
        {
            OnCoinCollide?.Invoke(this, EventArgs.Empty);
            collider.gameObject.SetActive(false);
        }

        if (collider.gameObject.CompareTag("Car") && !unTouchable)
        {
            currentHealth--;
            if (currentHealth > 0)
            {
                SetActiveFalse(false);
                StartCoroutine(WaitAndColorChange(unTouchableTime));
            }
            collider.gameObject.SetActive(false);
        }

        if (collider.gameObject.CompareTag("TrainEdge"))
        {
            collider.GetComponentInParent<TrainObstacle>().gameObject.SetActive(false);
            currentHealth--;
            if (currentHealth > 0)
            {
                SetActiveFalse(false);
                StartCoroutine(WaitAndColorChange(unTouchableTime));
            }
            collider.gameObject.SetActive(false);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EndLine"))
        {
            other.transform.parent.position = new Vector3(0, 0, other.transform.parent.position.z + 200);
        }
    }

    IEnumerator WaitAndColorChange(float waitTime)
    {
        unTouchable = true;

        for (int i = 0; i < 3; i++)
        {
            SetActiveFalse(true);
            yield return new WaitForSeconds(waitTime);

            SetActiveFalse(false);
            yield return new WaitForSeconds(waitTime);
        }
        SetActiveFalse(true);
        yield return new WaitForSeconds(waitTime);

        unTouchable = false;
    }

    private void SetActiveFalse(bool isActive)
    {
        deathAnimationObjects.SetActive(isActive);
    }
}
