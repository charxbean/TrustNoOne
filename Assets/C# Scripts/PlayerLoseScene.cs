using Unity.VisualScripting;
using UnityEngine;

public class PlayerLoseScene : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Header("Shape Sprites")]
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite squareSprite;
    [SerializeField] private Sprite triangleSprite;
    [SerializeField] private Sprite heartSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (PlayerBehavior.currentShape)
        {
            case 0: 
                spriteRenderer.sprite = circleSprite;
                gameObject.tag = "Circle";
                break;
            case 1: 
                spriteRenderer.sprite = squareSprite;
                gameObject.tag = "Square";
                break;
            case 2: 
                spriteRenderer.sprite = triangleSprite;
                gameObject.tag = "Triangle";
                break;
            case 3: 
                spriteRenderer.sprite = heartSprite;
                gameObject.tag = "Heart";
                break;              
            default:
                Debug.Log("playerLoseScene: not a valid shape tage");
                break;              
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
