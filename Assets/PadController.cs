using UnityEngine;

public class PadController : MonoBehaviour
{
[Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SpriteRenderer dpadRenderer; 
    [Header("Sprites")]
    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite up;
    [SerializeField] private Sprite down;
    [SerializeField] private Sprite left;
    [SerializeField] private Sprite right;

    private Sprite _lastSprite;

    private void Awake()
    {
        if (dpadRenderer == null) dpadRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // if(playerMovement.isDead == false)
        // {
        //     UpdateDPadVisual();
        // }

        UpdateDPadVisual();
    }

    private void UpdateDPadVisual()
    {
        Vector2 input = playerMovement.movementInput;

        bool horizontalStronger = Mathf.Abs(input.x) > Mathf.Abs(input.y);
        
        Sprite newSprite = idle;

        if (input.sqrMagnitude > 0.01f) 
        {
            if (horizontalStronger)
            {
                newSprite = (input.x > 0) ? right : left;
            }
            else
            {
                newSprite = (input.y > 0) ? up : down;
            }
        }

        if (newSprite != _lastSprite)
        {
            dpadRenderer.sprite = newSprite;
            _lastSprite = newSprite;
        }
    }
}
