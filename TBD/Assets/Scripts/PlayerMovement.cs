using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Player movement and jump speeds")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float horizontalJumpPower;
    [SerializeField] private float dashSpeed;
    [Tooltip("--------------OTHER------------------")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float startDashTime;
    [SerializeField] private float startDashCooldown;

    // player body and collider
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;

    // wall jump
    private float horizontalInput;
    private float wallJumpCooldown;
    // ------------------------------
    private Animator anim;
    private bool freezeRotation = true;

    // Dash
    private float dashCooldown;
    private float currentDashTime;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // flip player when moving left-right
        if(horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0);
        } else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0);
        }

        // Jump 
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isGrounded())
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0);
        }

        if(wallJumpCooldown > 0.2f)
        {
            // Controlled by a and d or arrowLeft and arrowRight
            body.linearVelocity = new Vector2(horizontalInput * movementSpeed, body.linearVelocity.y);
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 5;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 2.5f;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
                Jump();
            }

            
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        if (dashCooldown <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(Dash(Mathf.Sign(transform.localScale.x)));
                dashCooldown = startDashCooldown;
            }
        } else
        {
                dashCooldown -= Time.deltaTime;
        }
            if (freezeRotation)
            {
                body.freezeRotation = true;
            }
            else
            {
                body.freezeRotation = false;
            }

        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
    }

    private bool isGrounded()
    {
        // detect whether there is a ground layer below player
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        // detect whether there is a ground layer below player
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, 
            new Vector2(transform.localScale.x, 0), 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void Jump()
    {
        if(isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
            anim.SetTrigger("jump");
        } 
        else if(onWall() && !isGrounded())
        {
            wallJumpCooldown = 0;

            body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * horizontalJumpPower, jumpSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private IEnumerator Dash(float horizontalDirection)
    {
        currentDashTime = startDashTime;
        anim.SetBool("dashEnded", false);
        anim.SetTrigger("dash");

        while (currentDashTime > 0)
        {
            currentDashTime -= Time.deltaTime;

            body.linearVelocity = new Vector2(horizontalDirection * dashSpeed, 0);

            yield return null;
        }

        body.linearVelocity = new Vector2(0f, 0f);
        anim.SetBool("dashEnded", true);
    }   
}
