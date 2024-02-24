using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    // Oyuncunun hareketlerini kontrol eder.
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private CapsuleCollider playerHitBox;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private GameObject groundCheck;
    private float currentPoint = 0;
    private float nextPoint = 2.5f;
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private PlayerCollision playerCollision;
    private bool isDead = false;
    private float colliderSlideHigh = 0.5f;
    private float colliderRunHigh = 2.0f;
    private float colliderSlideDuration = 0.75f;
    [SerializeField] private float moveXDuration = 0.1f;

    public float PlayerSpeed
    {
        get { return playerSpeed; }
        set { playerSpeed = value; }
    }


    private void Start()
    {
        InputHandling.Instance.OnJump += OnJumpHandling;
        InputHandling.Instance.OnSlip += OnSlipHandling;
        InputHandling.Instance.OnMoveRight += OnMoveRightHandling;
        InputHandling.Instance.OnMoveLeft += OnMoveLeftHandling;
        playerCollision.OnPlayerDeath += OnPlayerDeathHandling;
    }

    private void OnPlayerDeathHandling(object sender, EventArgs e)
    {
       isDead = true;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);
    }

    private void OnJumpHandling(object sender, EventArgs e)
    {
        if (IsGrounded() && !isDead)
        {
            animator.SetTrigger("Jump");
            playerRb.AddForce(Vector3.up * 7.5f, ForceMode.Impulse);
        }
    }

    private void OnSlipHandling(object sender, EventArgs e)
    {
        if (IsGrounded() && !isDead)
        {
            animator.SetTrigger("Slide");
            StartCoroutine(ChanceColliderHigh());
        }
    }

    IEnumerator ChanceColliderHigh()
    {
        playerHitBox.height = colliderSlideHigh;

        yield return new WaitForSeconds(colliderSlideDuration);

        playerHitBox.height = colliderRunHigh;
        
    }

    private void OnMoveRightHandling(object sender, EventArgs e)
    {
        if (!isDead)
        {
            currentPoint += nextPoint;
            currentPoint = Mathf.Clamp(currentPoint, -2.5f, 2.5f);
            if (currentPoint == transform.position.x)
            {
                return;
            }
            StartCoroutine(PlayerMoveX(new Vector3(currentPoint, transform.position.y, transform.position.z), moveXDuration));
        }
    }

    private void OnMoveLeftHandling(object sender, EventArgs e)
    {
        if (!isDead)
        {
            currentPoint -= nextPoint;
            currentPoint = Mathf.Clamp(currentPoint, -2.5f, 2.5f);
            if (currentPoint == transform.position.x)
            {
                return;
            }
            StartCoroutine(PlayerMoveX(new Vector3(currentPoint, transform.position.y, transform.position.z), moveXDuration));
        }
    }

    IEnumerator PlayerMoveX(Vector3 currentPoint, float duration)
    {
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = new Vector3(currentPoint.x, transform.position.y, transform.position.z + 1.5f);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    private bool IsGrounded()
    {
      return Physics.CheckSphere(groundCheck.transform.position, 0.1f, groundLayerMask);

    }
}
