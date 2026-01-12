using UnityEngine;

public class GameboyAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject dpadPlaceHolder;
    [SerializeField] private GameObject dPadActive;

    [SerializeField] private GameObject btnPlaceHolder;
    [SerializeField] private GameObject btnActive;
    public void OnAnimationComplete()
    {
        UIManager.Instance.ShowMainMenu();
        dpadPlaceHolder.SetActive(false);
        dPadActive.SetActive(true);

        btnPlaceHolder.SetActive(false);
        btnActive.SetActive(true);
    }
}
