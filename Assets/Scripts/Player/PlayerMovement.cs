using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]

    private Rigidbody2D playerRigidbody;
    
    public float speed; // Speed of Player.
    private float moveHor; // Moving on the ground.
    private float moveVer; // May not use? Maybe for climbing.
    public float jump; // Can jump.
    public float jumpTime; // How long the player jumps before falling back to the ground.

    private float jumpCounter; // Determins the amount of time before the player stops jumping.
    private bool stoppedJumping; // Determins when the player can and cannot jump again.

    [Header("Player Grounded?")]

    public bool grounded; // Is the player grounded?
    public LayerMask whatIsGround; // Determins what the ground is in the game.
    public Transform groundCheck; // Checks to see if the player is grounded or not.
    public float groundCheckRadius; /* A small radius underneath the player to check if the player is
    grounded or not. */

    private Animator myAnimator;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>(); // Find the rigidbody2D class on an object.
        jumpCounter = jump; // Player can jump at the start of the game.
        stoppedJumping = true; // The player at the start of the game is said to be not jumping.
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            playerRigidbody.velocity = new Vector2(speed, playerRigidbody.velocity.y);
            transform.localScale = new Vector2(5, 5);

        }

        if (Input.GetKey(KeyCode.A))
        {
            playerRigidbody.velocity = new Vector2(-speed, playerRigidbody.velocity.y);
            transform.localScale = new Vector2(-5, 5);
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        // 1. The gound check positon on the player.
        // 2. The radius given to the ground check on the player.
        // 3. If the radius of ground check is overlapping the ground layer, then the player is grounded.

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            /* It asks the question is something happening, such as an inbput of a key.
             */
            if (grounded /* Grounded is a boolean. */ )
            {

                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jump);
                stoppedJumping = false;
            }

        }


        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {

            if (jumpCounter > 0)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jump);
                jumpCounter -= Time.deltaTime;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {

            jumpCounter = 0;
            stoppedJumping = true;

        }

        if (grounded)
        {
            jumpCounter = jumpTime;

        }

        myAnimator.SetFloat("Speed", playerRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);

    }

}
