using System;
using System.Collections;
using UnityEngine;

public class DecoyShapeBehavior : MonoBehaviour
{
    //Use coroutines to determine timing + delay of scripts
    
    public static event Action OnShowDecoyComplete;
    private SpriteRenderer spriteRenderer;

    public float waitBeforeSeconds = 2f;
    public float showSeconds = 2f;
    public float waitSeconds = 5f;
    private string currentTag;

    [Header("Shape Sprites")]
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite squareSprite;
    [SerializeField] private Sprite triangleSprite;
    [SerializeField] private Sprite heartSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int randShape = UnityEngine.Random.Range(0, 4);
    
        switch (randShape)
        {
            case 0:
                gameObject.tag = ("Circle");
                break;
                
            case 1: 
                gameObject.tag = ("Square");
                break;
            case 2: 
                gameObject.tag = ("Triangle");
                break;
            case 3: 
                gameObject.tag = ("Heart");
                break;
            default:
                Debug.Log("FallingObject: Not a real shape to switch");
                break;
                
        }
        currentTag = gameObject.tag;
        switchShape(currentTag);
        spriteRenderer.enabled = false;

        StartCoroutine(ShowDecoy());
    }


    void switchShape(string tag)
    {
        if(tag == "Circle")
        {
            Debug.Log("0");
            //spriteRenderer.sprite = circleSprite;
        }
        else if(tag == "Square")
        {
            Debug.Log("1");
            //spriteRenderer.sprite = squareSprite;
        }
        else if(tag == "Triangle")
        {
            Debug.Log("2");
            //spriteRenderer.sprite = triangleSprite;
        }
        else if(tag == "Heart")
        {
            Debug.Log("3");
            //spriteRenderer.sprite = heartSprite;
        }
    }

    IEnumerator ShowDecoy()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitBeforeSeconds);

            Debug.Log("Script A starting");
            spriteRenderer.enabled = true;
            
            yield return new WaitForSeconds(showSeconds);

            spriteRenderer.enabled = false;

            OnShowDecoyComplete?.Invoke();

            yield return new WaitForSeconds(waitSeconds);

            Debug.Log("Invoke");

        }

    }
}
