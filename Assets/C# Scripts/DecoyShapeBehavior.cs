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
    private int prevPrevShape;
    private int prevShape;

    [Header("Shape Sprites")]
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite squareSprite;
    [SerializeField] private Sprite triangleSprite;
    [SerializeField] private Sprite heartSprite;

    private int randShape;

    void Start()
    {

    }

    public void StartDecoy()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //spriteRenderer.enabled = false;

        StartCoroutine(ShowDecoy());
    }

    public void StartTutorial()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //spriteRenderer.enabled = true;
    }

    public void StopDecoy()
    {
        StopAllCoroutines();
    }

    public void revealDecoy()
    {
        gameObject.tag = ("Triangle");
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = triangleSprite;
    }
    IEnumerator ShowDecoy()
    {
        while (true)
        {
            if(GameStageManager.gameStage == 0)
            {
                gameObject.tag = ("Triangle");
                spriteRenderer.sprite = triangleSprite;
            }
            else
            {
                randShape = UnityEngine.Random.Range(0, 4);
                if(randShape == prevShape && prevShape == prevPrevShape)
                {
                    if(randShape == 3)
                    {
                        randShape = 0;
                    }
                    else
                    {
                        randShape ++;
                    }
                }
                prevPrevShape = prevShape;
                prevShape = randShape;

                switch (randShape)
                {
                    case 0:
                        gameObject.tag = ("Circle");
                        spriteRenderer.sprite = circleSprite;
                        break;
                        
                    case 1: 
                        gameObject.tag = ("Square");
                        spriteRenderer.sprite = squareSprite;
                        break;
                    case 2: 
                        gameObject.tag = ("Triangle");
                        spriteRenderer.sprite = triangleSprite;
                        break;
                    case 3: 
                        gameObject.tag = ("Heart");
                        spriteRenderer.sprite = heartSprite;
                        break;
                    default:
                        Debug.Log("Not a shape (DecoyShape Behavior)");
                        break;
                        
                }
            }

            currentTag = gameObject.tag;

            yield return new WaitForSeconds(waitBeforeSeconds);
            spriteRenderer.enabled = true;
            
            yield return new WaitForSeconds(showSeconds);

            spriteRenderer.enabled = false;

            OnShowDecoyComplete?.Invoke();

            yield return new WaitForSeconds(waitSeconds);

        }

    }
}
