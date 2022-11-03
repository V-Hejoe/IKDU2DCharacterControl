using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 playerMovement;
    private Rigidbody2D playerRB;
    private SpriteRenderer playerSR;
    private Animator playerAnimator;
    private int collectibleCount = 0;
    public int moveSpeed = 10;
    public Color[] playerColors;

    // Start is called before the first frame update
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        playerRB.velocity = playerMovement * moveSpeed;
    }

    private void OnMovement(InputValue moveValue)
    {
        playerMovement = moveValue.Get<Vector2>();

        if (playerMovement.x != 0 || playerMovement.y != 0)
        {
            playerAnimator.SetFloat("x", playerMovement.x);
            playerAnimator.SetFloat("y", playerMovement.y);
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectible")
        {
            collectibleCount++;
            playerSR.color = playerColors[collectibleCount];
            Destroy(other.gameObject);
        }
    }
}