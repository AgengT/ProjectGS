using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;

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
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void EnterGame()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void ShowGameFinishedUI()
    {
        // Implementation for showing game finished UI
    }
}
