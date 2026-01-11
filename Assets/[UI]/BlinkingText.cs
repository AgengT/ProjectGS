using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class BlinkingText : MonoBehaviour
{
[Header("Settings")]
    [SerializeField] private float blinkDuration = 0.5f;   // Time to fade out
    [SerializeField] private float minAlpha = 0.0f;        // 0 = Invisible, 1 = Fully Visible
    [SerializeField] private Ease blinkEase = Ease.Linear; // Linear is standard for blinking

    private void Start()
    {
        // TRY to get TextMeshPro component first (Preferred)
        var tmpText = GetComponent<TextMeshProUGUI>();
        
        if (tmpText != null)
        {
            // Blink TextMeshPro
            tmpText.DOFade(minAlpha, blinkDuration)
                .SetLoops(-1, LoopType.Yoyo) // -1 = Infinite loop, Yoyo = Back and Forth
                .SetEase(blinkEase);
        }
        else
        {
            // Fallback to standard Unity Text
            var legacyText = GetComponent<Text>();
            if (legacyText != null)
            {
                legacyText.DOFade(minAlpha, blinkDuration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(blinkEase);
            }
        }
    }
}
