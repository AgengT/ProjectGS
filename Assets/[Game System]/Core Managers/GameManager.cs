using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentGameState { get; private set; }
    public float gameTimer { get; private set; }
    
    private PlayerInputAction inputActions;
    public PlayerInputAction InputActions => inputActions; 
    
    private PlayerMovement cachedPlayer;
    
    [SerializeField] private GameObject levelManager;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject finalRoomUI;
    [SerializeField] private UpdateTimeText time;
    public Action<float> onTimerUpdate;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        inputActions = new PlayerInputAction();
        Cursor.visible = false;
    }
    
    private void Start()
    {
        inputActions.Player.OpenPause.performed += _ => TogglePause();
        inputActions.UI.Close.performed += _ => TogglePause();

        inputActions.UI.Select.performed += ctx => 
        {
            if (CurrentGameState != GameState.Playing)
            {
        
                Debug.Log("UI Confirm pressed");
            }
        };

        cachedPlayer = playerMovement; 
        SetGameState(GameState.MainMenu);
    }
    
    private void OnEnable() => inputActions?.Enable();
    private void OnDisable() => inputActions?.Disable();
    
    private void Update()
    {
        if (CurrentGameState == GameState.Playing)
        {
            gameTimer += Time.deltaTime;
        }
    }
    
    public void TogglePause()
    {
        if (CurrentGameState == GameState.MainMenu || CurrentGameState == GameState.GameFinished) 
            return;
            
        bool isPaused = CurrentGameState == GameState.Paused;
        SetGameState(isPaused ? GameState.Playing : GameState.Paused);
        
        if (isPaused)
        {
            UIManager.Instance.HidePauseMenu();
            SwitchInputToPlayer();
        }
        else
        {
            UIManager.Instance.ShowPauseMenu();
        }
    }
    
    public void SetGameState(GameState newState)
    {
        CurrentGameState = newState;
        
        // Time.timeScale = newState == GameState.Paused ? 0f : 1f;
        finalRoomUI.SetActive(newState == GameState.GameFinished);
        
        switch (newState)
        {
            case GameState.MainMenu:
                gameTimer = 0f;
                SwitchInputToUI();
                break;
            case GameState.Paused:
                SwitchInputToUI();
                break;
            case GameState.GameFinished:
                time.UpdateTime(gameTimer);
                break;
            case GameState.Playing:
                SwitchInputToPlayer();
                break;
        }
    }
    
    public void StartGame()
    {
        levelManager.SetActive(true);
        LevelManager.Instance.ResetLevels();
        gameTimer = 0f;
        SetGameState(GameState.Playing);
        LevelManager.Instance.LoadLevel(0);
        
        if (cachedPlayer != null)
        {
            cachedPlayer.GetComponent<SpriteRenderer>().enabled = true;
            cachedPlayer.EnableInput();
        }
    }
    
    public void RetryGame() => StartGame();
    
    public void SwitchInputToUI()
    {
        inputActions.Player.Disable();
        inputActions.UI.Enable();
        cachedPlayer?.DisableInput();
    }
    
    public void SwitchInputToPlayer()
    {
        inputActions.UI.Disable();
        inputActions.Player.Enable();
        cachedPlayer?.EnableInput(); 
    }
}

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameFinished
}
