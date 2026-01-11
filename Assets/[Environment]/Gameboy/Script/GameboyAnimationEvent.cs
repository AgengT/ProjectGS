using UnityEngine;

public class GameboyAnimationEvent : MonoBehaviour
{
    public void OnAnimationComplete()
    {
        UIManager.Instance.ShowMainMenu();
    }
}
