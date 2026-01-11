using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentGameState { get; private set; }
    public float gameTimer {get; private set;}

    private PlayerInputAction inputActions;
    [SerializeField] private GameObject levelManager;
    [SerializeField] private PlayerMovement playerMovement;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        inputActions = new PlayerInputAction();
    }

 
    private void Start()
    {
        inputActions.Player.OpenPause.performed += _ => TogglePause();
        
        inputActions.UI.Close.performed += _ => TogglePause();

        SetGameState(GameState.MainMenu);
    }

    private void OnEnable()
    {
        inputActions?.Enable();
    }

    private void OnDisable()
    {
        inputActions?.Disable();
    }

    private void Update()
    {
        if (CurrentGameState == GameState.Playing)
        {
            gameTimer += Time.deltaTime;
        }
    }

    public void TogglePause()
    {
        if (CurrentGameState == GameState.MainMenu || CurrentGameState == GameState.GameFinished) return;

        if (CurrentGameState == GameState.Playing)
        {
            SetGameState(GameState.Paused);
            UIManager.Instance.ShowPauseMenu();
        }
        else if (CurrentGameState == GameState.Paused)
        {
            SetGameState(GameState.Playing);
            UIManager.Instance.HidePauseMenu();
            SwitchInputToPlayer(); 
        }
    }

    public void SetGameState(GameState newState)
    {
        CurrentGameState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                gameTimer = 0f;
                SwitchInputToUI();
                break;
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                SwitchInputToUI();
                break;
            case GameState.GameFinished:
                Time.timeScale = 1f;
                break;
        }
    }

    public void StartGame()
    {
        levelManager.SetActive(true);
        LevelManager.Instance.ResetLevels();

        gameTimer = 0f;

        SetGameState(GameState.Playing);
        SwitchInputToPlayer();

        inputActions.Player.Enable();
        inputActions.UI.Disable();

        LevelManager.Instance.LoadLevel(0);   

        playerMovement.GetComponent<SpriteRenderer>().enabled = true;
        playerMovement.EnableInput();
    }

    public void RetryGame()
    {
        StartGame();
    }

    public void SwitchInputToUI()
    {
        inputActions.Player.Disable();
        inputActions.UI.Enable();

        var player = FindFirstObjectByType<PlayerMovement>();
        if (player != null) player.DisableInput();
    }

    public void SwitchInputToPlayer()
    {
        inputActions.UI.Disable();
        inputActions.Player.Enable();

        var player = FindFirstObjectByType<PlayerMovement>();
        if (player != null) player.EnableInput();
    }
}

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameFinished
}
