using UnityEngine;

public class UpdateTimeText : MonoBehaviour
{
   [SerializeField] private TMPro.TextMeshProUGUI timeText;
   public void UpdateTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
