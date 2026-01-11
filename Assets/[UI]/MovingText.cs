using UnityEngine;
using DG.Tweening;

public class MovingText : MonoBehaviour
{
[Header("Settings")]
    [SerializeField] private bool startMovingRight = true;
    [SerializeField] private float moveDistance = 300f; // The "Max Distance"
    [SerializeField] private float moveSpeed = 100f;    // Speed in units per second (Constant speed)
    [SerializeField] private float pauseTime = 0.5f;    // Time to wait at edges

    [Header("Ease")]
    [SerializeField] private Ease motionEase = Ease.InOutQuad;

    private RectTransform _rectTransform;
    private Vector2 _centerPos;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _centerPos = _rectTransform.anchoredPosition;
    }

    private void Start()
    {
        StartPatrol();
    }

    public void StartPatrol()
    {
        float rightEdgeX = _centerPos.x + moveDistance;
        float leftEdgeX = _centerPos.x - moveDistance;

        float firstTargetX = startMovingRight ? rightEdgeX : leftEdgeX;
        float secondTargetX = startMovingRight ? leftEdgeX : rightEdgeX;

        float introDuration = moveDistance / moveSpeed;        
        float fullDuration = (moveDistance * 2) / moveSpeed;   

        _rectTransform.DOAnchorPosX(firstTargetX, introDuration)
            .SetEase(motionEase)
            .OnComplete(() => 
            {
                Sequence loopSequence = DOTween.Sequence();
                loopSequence.AppendInterval(pauseTime);
                loopSequence.Append(_rectTransform.DOAnchorPosX(secondTargetX, fullDuration).SetEase(motionEase));
                loopSequence.AppendInterval(pauseTime);
                loopSequence.Append(_rectTransform.DOAnchorPosX(firstTargetX, fullDuration).SetEase(motionEase));
                loopSequence.SetLoops(-1);
            });
    }
}
