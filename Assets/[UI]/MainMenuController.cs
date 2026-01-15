using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private PlayerInputAction inputActions;
    private void Awake()
    {
        inputActions = new PlayerInputAction();
    }

    private void OnEnable()
    {
        inputActions.UI.Select.performed += OnSelectPerformed;
    }

    private void OnDisable()
    {
        inputActions.UI.Select.performed -= OnSelectPerformed;
        inputActions.UI.Disable();
    }

    public void EnableMenuInput()
    {
        inputActions.UI.Enable();
    }

    public void DisableMenuInput(){
        inputActions.UI.Disable();
    }

    private void OnSelectPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        UIManager.Instance.EnterGame();
    }
}