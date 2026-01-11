using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private CanvasGroup mainMenu;
    [SerializeField] private CanvasGroup pauseMenu;

    public bool isUIActive { get; private set; } = false;

    private float fadeDuration = 1f;
    private float starDelay = 0.5f;
    private Ease fadeEase = Ease.OutQuad;

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

    private void Start()
    {
        // ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        isUIActive = true;
        AudioManager.PlayLoop("BackgroundMusic");
        mainMenu.DOFade(1f, fadeDuration)
            .SetDelay(starDelay)
            .SetEase(fadeEase)
            .OnComplete(() => 
            {
                mainMenu.interactable = true;
                mainMenu.blocksRaycasts = true;
            }
            );
    }

    public void EnterGame()
    {
        isUIActive = false;
        mainMenu.DOFade(0f, fadeDuration)
            .SetEase(fadeEase)
            .OnComplete(() => 
            {
                mainMenu.interactable = false;
                mainMenu.blocksRaycasts = false;
            }
            );
    }

    public void ShowPauseMenu()
    {
        isUIActive = true;
        pauseMenu.alpha = 1f;
        pauseMenu.interactable = true;
        pauseMenu.blocksRaycasts = true;
    }

    public void HidePauseMenu()
    {
        isUIActive = false;
        pauseMenu.alpha = 0f;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;
    }

    public void ShowGameFinishedUI()
    {
        // Implementation for showing game finished UI
    }
}
