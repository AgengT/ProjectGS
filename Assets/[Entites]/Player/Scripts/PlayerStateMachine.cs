using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Walk_Horizontal,
        Walk_Up,
        Walk_Down,
        Dead
    }

    private PlayerState currentState;

    private PlayerMovement playerMovement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        Vector2 movementInput = playerMovement.movementInput;

        PlayerState newState = currentState;

        if (playerMovement.isDead)
        {
            newState = PlayerState.Dead;
        }
        else
        {
            if (movementInput.x != 0)
            {
                newState = PlayerState.Walk_Horizontal;
                spriteRenderer.flipX = movementInput.x < 0;
            }
            else if (movementInput.y > 0)
            {
                newState = PlayerState.Walk_Up;
            }
            else if (movementInput.y < 0)
            {
                newState = PlayerState.Walk_Down;
            }
            else
            {
                newState = PlayerState.Idle;
            }
        }

        if (newState != currentState)
        {
            currentState = newState;
            animator.Play(currentState.ToString());
        }
    }
}