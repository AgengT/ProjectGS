using UnityEngine;

public class GameboyAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject dpadPlaceHolder;
    [SerializeField] private GameObject dPadActive;
    public void OnAnimationComplete()
    {
        UIManager.Instance.ShowMainMenu();
        dpadPlaceHolder.SetActive(false);
        dPadActive.SetActive(true);
    }
}
