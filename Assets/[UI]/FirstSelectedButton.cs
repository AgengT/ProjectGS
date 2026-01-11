using UnityEngine;

public class FirstSelectedButton : MonoBehaviour
{
    [SerializeField] private GameObject firstSelectedButton;

    private void OnEnable()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        if(firstSelectedButton != null) 
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }
}
