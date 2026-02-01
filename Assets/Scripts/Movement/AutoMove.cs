using UnityEngine;
using System.Collections;

public class PatrolController : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float speed = 5f;
    public Sprite baseSprite;
    public float wobbleAngle = 3f;
    public float wobbleSpeed = 4f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 targetPoint;
    private Vector2 pointA;
    private Vector2 pointB;
    private float wobbleTime;
    private bool isPatrolling;
    private Coroutine patrolCoroutine;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = baseSprite;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    public void StartPatrol(Vector2 a, Vector2 b)
    {
        StopPatrol();

        pointA = a;
        pointB = b;
        patrolCoroutine = StartCoroutine(PatrolRoutine());
    }

    public void StopPatrol()
    {
        if (patrolCoroutine != null)
            StopCoroutine(patrolCoroutine);

        patrolCoroutine = null;
        isPatrolling = false;
        rb.linearVelocity = Vector2.zero;
        wobbleTime = 0f;
        transform.rotation = Quaternion.identity;
    }

    private IEnumerator PatrolRoutine()
    {
        isPatrolling = true;
        targetPoint = pointB;

        while (isPatrolling)
        {
            Vector2 currentPos = rb.position;
            Vector2 dir = (targetPoint - currentPos).normalized;

            rb.MovePosition(currentPos + dir * speed * Time.deltaTime);

            if (Mathf.Abs(dir.x) > 0.01f)
                spriteRenderer.flipX = dir.x < 0;

            wobbleTime += Time.deltaTime * wobbleSpeed;
            float wobble = Mathf.Sin(wobbleTime) * wobbleAngle;
            transform.rotation = Quaternion.Euler(0, 0, wobble);

            if (Vector2.Distance(currentPos, targetPoint) < 0.1f)
                targetPoint = (targetPoint == pointA) ? pointB : pointA;

            yield return null;
        }
    }
}