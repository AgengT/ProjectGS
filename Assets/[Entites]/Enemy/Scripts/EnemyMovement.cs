using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Patrol Mode Settings")]
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;

    [Header("Override Mode Settings")]
    [SerializeField] private bool overrideHorizontalMovement = false;
    [SerializeField] private bool overrideVerticalMovement = false;

    [Header("General Settings")]
    [SerializeField] private float movingSpeed = 2f;
    [SerializeField] private bool reverseDirection = false;

    // State Variables
    private Rigidbody2D rb;
    private Vector2 currentTarget;      // Used for Patrol Mode
    private Vector2 moveDirection;      // Used for Override Mode
    private bool isOverrideActive;      // Tracks which mode we are in
    
    private float lastBounceTime;
    private float bounceCooldown = 0.1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Check if we are using the new "Override" mode
        isOverrideActive = overrideHorizontalMovement || overrideVerticalMovement;

        if (isOverrideActive)
        {
            // --- OVERRIDE INITIALIZATION ---
            // 1. Determine base direction (Right or Up)
            float x = overrideHorizontalMovement ? 1f : 0f;
            float y = overrideVerticalMovement ? 1f : 0f;
            moveDirection = new Vector2(x, y);

            // 2. Apply Reverse (Left or Down)
            if (reverseDirection)
            {
                moveDirection *= -1; 
            }
        }
        else
        {
            // --- PATROL INITIALIZATION (Old Logic) ---
            // Safety check for missing transforms
            if (startPos == null || endPos == null)
            {
                Debug.LogError("StartPos or EndPos is missing on " + gameObject.name);
                enabled = false;
                return;
            }

            if (reverseDirection)
            {
                rb.position = endPos.position;
                currentTarget = startPos.position;
            }
            else
            {
                rb.position = startPos.position;
                currentTarget = endPos.position;
            }
        }
    }

    private void FixedUpdate()
    {
        // LOGIC SPLIT: Are we Overriding or Patrolling?
        if (isOverrideActive)
        {
            // --- OVERRIDE MOVEMENT ---
            // Just move endlessly in the calculated direction
            Vector2 displacement = moveDirection * movingSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + displacement);
        }
        else
        {
            // --- PATROL MOVEMENT ---
            // Check distance to specific target point
            if(Vector2.Distance(rb.position, currentTarget) < 0.1f)
            {
                SwitchTarget();
            }

            Vector2 newPos = Vector2.MoveTowards(rb.position, currentTarget, movingSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bounds"))
        {
            if(Time.time < lastBounceTime + bounceCooldown) return;

            lastBounceTime = Time.time;
            SwitchTarget();
        }else if(collision.gameObject.CompareTag("Enemy"))
        {
            // Ignore collision with other enemies to prevent bouncing
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    private void SwitchTarget()
    {
        if (isOverrideActive)
        {
            // --- OVERRIDE SWITCH ---
            // Simply flip the vector (Right becomes Left, Up becomes Down)
            moveDirection *= -1;
        }
        else
        {
            // --- PATROL SWITCH ---
            // Swap between the two specific transforms
            if (currentTarget == (Vector2)endPos.position)
            {
                currentTarget = startPos.position;
            }
            else
            {
                currentTarget = endPos.position;
            }
        }
    }
}