using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 6f;
    //private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform playerVisuals; //creates ref to player sprite
    [SerializeField] private Animator playerAnimator;

    //Checks what scene the player is in and triggers fall animation in dream
    private void Start()
    {
        if (playerAnimator == null) return;
        //Checks if in dream scene
        bool isDreamSequence = SceneManager.GetActiveScene().name == "DreamSequence";
        //Sets IsFalling to be true if yes
        playerAnimator.SetBool("IsFalling", isDreamSequence);
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        playerAnimator.SetBool("IsWalking", horizontal != 0f); //Starts walking if horizontal speed exceeds 0
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //flips player sprite across the y-axis depending on the direction the player faces
    private void Flip()
    {
        //Condition: If player is facing right and moves left/If player is facing left and moves right
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = playerVisuals.localScale;
            localScale.x *= -1f;
            playerVisuals.localScale = localScale;
        }
    }
}