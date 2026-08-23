using UnityEngine;
using UnityEngine.UIElements;

public class FallingShapeBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float horizontalInput;
    //get the rb
    private Rigidbody2D rb; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(rb.position.x <= -11)
        {
            rb.position = new Vector2(15f, 0.5f);
        }
        rb.linearVelocityX = -1 * moveSpeed;
    }
}
