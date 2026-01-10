using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;

    [SerializeField] private float movingSpeed = 2f;
    [SerializeField] private bool reverseDirection = false;

    private Rigidbody2D rb;
    private Vector2 currentTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

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

    private void FixedUpdate()
    {
        if(Vector2.Distance(rb.position, currentTarget) < 0.1f)
        {
            if(currentTarget == (Vector2)endPos.position)
            {
                currentTarget = startPos.position;
            }
            else
            {
                currentTarget = endPos.position;
            }
        }

        Vector2 newPos = Vector2.MoveTowards(rb.position, currentTarget, movingSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }
}
