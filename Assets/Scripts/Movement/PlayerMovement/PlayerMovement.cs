using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Sprites (Left-facing defaults)")]
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;  // Flipped left sprite

    [Header("Wobble")]
    public float wobbleAngle = 3f;
    public float wobbleSpeed = 4f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private float wobbleTime = 0f;
    private string currentDirection = "left";  // Initial default

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteLeft;  // Start left
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // Update direction/sprite ONLY when moving
        if (movement.magnitude > 0)
        {
            string newDirection = GetDirection(movement);
            if (newDirection != currentDirection)
            {
                spriteRenderer.sprite = GetSprite(newDirection);
                currentDirection = newDirection;
            }

            // Wobble
            wobbleTime += Time.deltaTime * wobbleSpeed;
            float wobble = Mathf.Sin(wobbleTime) * wobbleAngle;
            transform.rotation = Quaternion.Euler(0, 0, wobble);
        }
        else
        {
            transform.rotation = Quaternion.identity;
            // NO reset: currentDirection stays as last moved!
        }
    }

    string GetDirection(Vector2 dir)
    {
        if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
            return dir.y > 0 ? "up" : "down";
        return dir.x > 0 ? "right" : "left";
    }

    Sprite GetSprite(string dir)
    {
        return dir switch
        {
            "up" => spriteUp,
            "down" => spriteDown,
            "right" => spriteRight,
            "left" => spriteLeft,
            _ => spriteLeft
        };
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
