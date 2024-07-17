using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isDead = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);

        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            Walk(horizontalInput);
        }
        else
        {
            Idle();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            Die();
        }
    }

    private void Walk(float direction)
    {
        rb.velocity = new Vector2(direction * walkSpeed, rb.velocity.y);
        animator.SetBool("isWalking", true);
        animator.SetBool("isJumping", false);

        if (direction > 0 && !isDead)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (direction < 0 && !isDead)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private void Idle()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetBool("isWalking", false);
        animator.SetBool("isJumping", false);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetBool("isJumping", true);
        animator.SetBool("isWalking", false);
    }

    private void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("isDead", true);
        // Opcional: Desactivar colisiones, entrada de usuario, etc.
    }
}
