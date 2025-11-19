using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20;

    private float horizontalInput;
    private float verticalInput;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Calculate the desired velocity vector once
        Vector2 moveVector = new Vector2(horizontalInput, verticalInput);

        // Normalize the vector to prevent diagonal speed boost (if necessary)
        // and then apply the speed.
        moveVector.Normalize();

        // Apply the movement using Rigidbody2D.velocity 
        // This is one clean operation that sets both X and Y simultaneously.
        rb.linearVelocity = moveVector * moveSpeed;
    }
}
