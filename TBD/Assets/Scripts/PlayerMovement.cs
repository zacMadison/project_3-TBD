using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float horizontalJumpPower;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private float wallJumpCooldown;
    private Animator anim;
    private bool freezeRotation = true;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
    horizontalInput = Input.GetAxis("Horizontal");
        // Controlled by a and d or arrowLeft and arrowRight
        body.linearVelocity = new Vector2(horizontalInput*speed, body.linearVelocity.y);

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
            if(onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 2.5f;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
                Jump();
            }

            // Controlled by a and d or arrowLeft and arrowRight
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
        

        // Dash
        if(Input.GetKey(KeyCode.Space))
        {
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed * 2, body.linearVelocity.y);
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
        else if(onWall())
        {
            wallJumpCooldown = 0;

            body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, jumpSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
