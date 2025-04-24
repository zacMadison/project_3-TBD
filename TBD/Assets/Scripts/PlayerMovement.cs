using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Controlled by a and d or arrowLeft and arrowRight
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal")*speed, body.linearVelocity.y);

        // Jump
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if(body.position.y <= 0)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, Input.GetAxis("Vertical") * speed);
            }
        }

        // Dash
        if(Input.GetKey(KeyCode.Space))
        {
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed * 2, body.linearVelocity.y);
        }
    }
}
