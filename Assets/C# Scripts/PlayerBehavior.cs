using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public static int currentShape;
    private SpriteRenderer spriteRenderer;
    [Header("Shape Sprites")]
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite squareSprite;
    [SerializeField] private Sprite triangleSprite;
    [SerializeField] private Sprite heartSprite;

    void Start()
    {
        currentShape = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentShape)
        {
            case 0:
                gameObject.tag = "Circle";
                break;
            case 1: 
                gameObject.tag = "Square";
                break;
            case 2:
                gameObject.tag = "Triangle";
                break;
            case 3:
                gameObject.tag = "Heart";
                break;
            default:
                Debug.Log("Not a shape (playerBehavior)");
                break;
        }

        Debug.Log("tag" + " " + gameObject.tag);
    }
}
