using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public bool isUIActive { get; private set; }
    
    [SerializeField] private CanvasGroup mainMenu;
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private MainMenuController mainMenuController;
    
    private const float FADE_DURATION = 0.5f;
    private const float START_DELAY = 0.5f;
    private const Ease FADE_EASE = Ease.OutQuad;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowMainMenu()
    {
        HidePauseMenu();
        if(GameManager.Instance.CurrentGameState == GameState.Playing)
            LevelManager.Instance.ResetLevels();
        GameManager.Instance.SetGameState(GameState.MainMenu);
        
        
        isUIActive = true;
        AudioManager.PlayLoop("BackgroundMusic");
        
        mainMenu.DOFade(1f, FADE_DURATION)
            .SetDelay(START_DELAY)
            .SetEase(FADE_EASE)
            .OnComplete(() => SetCanvasState(mainMenu, true));
    }

    public void EnterGame()
    {
        isUIActive = false;
        
        mainMenu.DOFade(0f, FADE_DURATION)
            .SetEase(FADE_EASE)
            .OnComplete(() =>
            {
                SetCanvasState(mainMenu, false);
                GameManager.Instance.StartGame();
            });
    }

    public void ShowPauseMenu()
    {
        isUIActive = true;
        SetCanvasState(pauseMenu, true);
    }

    public void HidePauseMenu()
    {
        isUIActive = false;
        SetCanvasState(pauseMenu, false);
    }

    // Helper method to reduce repetition
    private void SetCanvasState(CanvasGroup canvas, bool active)
    {
        canvas.alpha = active ? 1f : 0f;
        canvas.interactable = active;
        canvas.blocksRaycasts = active;
        
        if (active && canvas == mainMenu && mainMenuController != null)
            mainMenuController.EnableMenuInput();
    }
}
