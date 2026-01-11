using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentGameState { get; private set; }
    public float gameTimer {get; private set;}

    [SerializeField] private InputActionAsset playerInput;
    [SerializeField] private GameObject levelManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        SetGameState(GameState.MainMenu);
    }

    private void Update()
    {
        if (CurrentGameState == GameState.Playing)
        {
            gameTimer += Time.deltaTime;
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
                SwitchInputToUI();
                break;
        }
    }

    private void StartGame()
    {
        levelManager.SetActive(true);
        SetGameState(GameState.Playing);
        SwitchInputToPlayer();
    }

    private void SwitchInputToUI()
    {
        playerInput.FindActionMap("Player").Disable();
        playerInput.FindActionMap("UI").Enable();
    }

    private void SwitchInputToPlayer()
    {
        playerInput.FindActionMap("UI").Disable();
        playerInput.FindActionMap("Player").Enable();
    }
}

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameFinished
}
