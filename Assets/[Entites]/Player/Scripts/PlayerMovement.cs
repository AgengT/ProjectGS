using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInputAction inputActions {get; private set;}
    public Vector2 movementInput {get; private set;}
    public Rigidbody2D rb {get; private set;}

    [SerializeField] private float moveSpeed =2f;

    public bool isDead {get; private set;}

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // inputActions.Player.Enable();
    }

    private void FixedUpdate()
    {
        if(isDead){
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 movement = movementInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void OnEnable()
    {
        inputActions.Player.Movement.performed += OnMovementInput;
        inputActions.Player.Movement.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        inputActions.Player.Movement.performed -= OnMovementInput;
        inputActions.Player.Movement.canceled -= OnMovementCanceled;
        DisableInput();
    }

    public void EnableInput()
    {
        inputActions.Player.Enable();
    }

    public void DisableInput()
    {
        inputActions.Player.Disable();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDead) return;

        if(collision.gameObject.CompareTag("Enemy"))
        {
            PlayerDie();
        }
        else if(collision.gameObject.CompareTag("Finish"))
        {
            LevelManager.Instance.NextLevel();
            AudioManager.PlayOneShot("FinishLevel");
        }
        else if(collision.gameObject.CompareTag("Final")){
            GameManager.Instance.SetGameState(GameState.GameFinished);
            AudioManager.PlayOneShot("FinishGame");
            LevelManager.Instance.NextLevel();
        }
        else if (collision.gameObject.CompareTag("MainMenu"))
        {
            GameManager.Instance.SetGameState(GameState.MainMenu);
            UIManager.Instance.ShowMainMenu();
            LevelManager.Instance.ResetLevels();
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (collision.gameObject.CompareTag("Retry"))
        {
            GameManager.Instance.StartGame();
        }
        else
        {
            return;
        }
    }

    private void PlayerDie()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        AudioManager.PlayOneShot("PlayerDeath");

        var level = FindFirstObjectByType<Level>();
        if(level != null)
        {
            level.OnPlayerDeath();
        }
        else
        {
            Debug.LogError("Level script not found in the scene.");
        }
    }

    public void PlayerReset()
    {
        isDead = false;
        movementInput = Vector2.zero;
    }
}
