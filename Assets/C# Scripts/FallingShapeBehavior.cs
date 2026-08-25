using System;
using System.Collections;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FallingShapeBehavior : MonoBehaviour
{
    
    private float horizontalInput;
    private Rigidbody2D rb; 
    private string currentTag;
    private bool start = false;
    private bool triggered = false;

    public float moveSpeed = 5f;
    [SerializeField] private DecoyShapeBehavior DecoyShapeBehavior;

    [Header("Shape Sprites")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite squareSprite;
    [SerializeField] private Sprite triangleSprite;
    [SerializeField] private Sprite heartSprite;


    void Start()
    {

        
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        DecoyShapeBehavior.OnShowDecoyComplete += signalStart;
    }

    void OnDisable()
    {
        DecoyShapeBehavior.OnShowDecoyComplete -= signalStart;
    }
    // Update is called once per frame
    void Update()
    {
        currentTag = gameObject.tag;
    }

    void FixedUpdate()
    {
        if (start)
        {
            spriteRenderer.enabled = true;
            if(triggered)
            {
                spriteRenderer.enabled = false;
                triggered = false;
                
                rb.position = new Vector2(6f, 0.5f);
                //replace with finding a random shape
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
                        Debug.Log("Not a shape (FallingShape Behavior)");
                        break;
                    
                }
                currentTag = gameObject.tag;
                switchShape(currentTag);
                start = false;
            }
            rb.linearVelocityX = -1 * moveSpeed;
        }
        else
        {
            spriteRenderer.enabled = false;
            rb.linearVelocityX = 0;
        }

    }

    public void StopBehavior()
    {
        StopAllCoroutines();
    }

    void signalStart()
    {
        start = true;
        spriteRenderer.enabled = true;
    }

    void switchShape(string tag)
    {
        if(tag == "Circle")
        {
            //Debug.Log("Circle");
            spriteRenderer.sprite = circleSprite;
        }
        else if(tag == "Square")
        {
            //Debug.Log("Square");
            spriteRenderer.sprite = squareSprite;
        }
        else if(tag == "Triangle")
        {
            //Debug.Log("Triangle");
            spriteRenderer.sprite = triangleSprite;
        }
        else if(tag == "Heart")
        {
            //Debug.Log("Heart");
            spriteRenderer.sprite = heartSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggered = true;
        if(other.CompareTag(currentTag))
        {
            //Visual feedback if correct/incorrect
            Debug.Log("Correct shape");
        }
        else
        {
            //lose a life/end the game
            Debug.Log("Incorrect Shape");
            LivesScript.lives -= 1;
            Debug.Log("lives " + LivesScript.lives);
        }
    }
}


