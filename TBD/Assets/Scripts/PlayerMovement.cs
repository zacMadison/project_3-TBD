using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
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
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);    
        }

        // Dash
        if(Input.GetKey(KeyCode.Space))
        {
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed * 2, body.linearVelocity.y);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
