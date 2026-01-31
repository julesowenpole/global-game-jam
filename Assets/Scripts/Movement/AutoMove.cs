using UnityEngine;

public class AutoPatrolMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public Vector2 pointA = new Vector2(-5f, 0f);
    public Vector2 pointB = new Vector2(5f, 0f);

    [Header("Sprites (Left-facing defaults)")]
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    [Header("Wobble")]
    public float wobbleAngle = 3f;
    public float wobbleSpeed = 4f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 targetPoint;
    private float wobbleTime = 0f;
    private string currentDirection = "left";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteLeft;
        targetPoint = pointB;  // Start moving to B
    }

    void Update()
    {
        Vector2 currentPos = transform.position;
        Vector2 directionToTarget = (targetPoint - currentPos).normalized;
        
        // Update sprite/wobble
        string newDirection = GetDirection(directionToTarget);
        if (newDirection != currentDirection)
        {
            spriteRenderer.sprite = GetSprite(newDirection);
            currentDirection = newDirection;
        }

        wobbleTime += Time.deltaTime * wobbleSpeed;
        float wobble = Mathf.Sin(wobbleTime) * wobbleAngle;
        transform.rotation = Quaternion.Euler(0, 0, wobble);

        // Switch points when close
        if (Vector2.Distance(currentPos, targetPoint) < 0.1f)
        {
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = (targetPoint - rb.position).normalized;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
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
}
