using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer buttonRenderer;
    
    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite pressedSprite;
    
    [Header("Settings")]
    [SerializeField] private string action = "Select";
    
    private Sprite _lastSprite;
    
    private void Awake()
    {
        if (buttonRenderer == null) 
            buttonRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        UpdateButtonVisual();
    }
    
    private void UpdateButtonVisual()
    {
        if (GameManager.Instance == null || GameManager.Instance.InputActions == null) 
            return;
        
        bool isPressed = false;
        
        switch (action)
        {
            case "Select":
                isPressed = GameManager.Instance.InputActions.UI.Select.IsPressed();
                break;
            case "Close":
                isPressed = GameManager.Instance.InputActions.UI.Close.IsPressed();
                break;
        }
        
        Sprite newSprite = isPressed ? pressedSprite : normalSprite;
        
        if (newSprite != _lastSprite)
        {
            buttonRenderer.sprite = newSprite;
            _lastSprite = newSprite;
        }
    }
}
