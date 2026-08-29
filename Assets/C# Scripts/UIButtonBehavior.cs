using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIButtonBehavior : MonoBehaviour
{
    public int thisButtonsShape;
    public static bool buttonClicked = false;
    public float moveSpeed = 2f;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private Button thisButton;
    
    [Header("Alt UI Sprites")]
    
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite squareSprite;
    [SerializeField] private Sprite triangleSprite;
    [SerializeField] private Sprite heartSprite;

    [Header("Keyboard Button")]

    [SerializeField] private Key keyBeforeSwitch;
    [SerializeField] private Key keyAfterSwitch;
    [SerializeField] private GameStageManager gameStageManager;

    private Color normalColor;
    private Color pressedColor;


    //set global var to a number

    void Start()
    {
        ColorBlock colors = thisButton.colors; 
        normalColor = colors.normalColor;
        pressedColor = colors.pressedColor;
    }
    void Update()
    {
        KeyboardButtons();
    }
    //ON CLICK BEHAVIOR
    public void SwitchShape()
    {
        audioManager.playSFX(audioManager.buttonPress);
        PlayerBehavior.currentShape = thisButtonsShape;
        buttonClicked = true;
        
    }

    void KeyboardButtons()
    {
        if (GameStageManager.gameStage < 3 && Keyboard.current[keyBeforeSwitch].wasPressedThisFrame)
        {
            thisButton.targetGraphic.color = pressedColor;
            SwitchShape();
            StartCoroutine(KeyboardPressVisual());
        }
        
        if (GameStageManager.gameStage >= 3 && Keyboard.current[keyAfterSwitch].wasPressedThisFrame)
        {

            thisButton.targetGraphic.color = pressedColor;
            SwitchShape();
            StartCoroutine(KeyboardPressVisual());
        }
    }

    private IEnumerator KeyboardPressVisual()
    {
        yield return new WaitForSeconds(.2f);

        thisButton.targetGraphic.color = normalColor;
    }

    void OnEnable()
    {
        GameStageManager.OnStage3Start += StartButtonRearrange;
        GameStageManager.OnStage5Start += StartButtonSwitchShapes;
    }

    public void StartButtonRearrange()
    {
        StartCoroutine(rearrangeButtons());
    }

    void OnDisable()
    {
        GameStageManager.OnStage3Start -= StartButtonRearrange;
        GameStageManager.OnStage5Start -= StartButtonSwitchShapes;
    }
    
    public void StartButtonSwitchShapes()
    {
        StartCoroutine(switchButtonColors());
    }
    IEnumerator rearrangeButtons(){
        RectTransform rect = GetComponent<RectTransform>();

        UnityEngine.Vector2 startPosition = rect.anchoredPosition;
        UnityEngine.Vector2 targetPosition = new UnityEngine.Vector2(182, -412);

        if(thisButtonsShape == 0)
        {
            targetPosition = new UnityEngine.Vector2(222.5794f, -425.0015f);
        }
        else if(thisButtonsShape == 1)
        {
            targetPosition = new UnityEngine.Vector2(-559.9954f, -425.0015f);
        }
        else if(thisButtonsShape == 2)
        {
            targetPosition = new UnityEngine.Vector2(598.0844f, -425.0015f);
        }
        else if(thisButtonsShape == 3)
        {
            targetPosition = new UnityEngine.Vector2(-174.9801f, -425.0015f);
        }
        else
        {
            Debug.Log("UIBUtton: not a real shape");
        }

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;

            rect.anchoredPosition =
                UnityEngine.Vector2.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        rect.anchoredPosition = targetPosition;

        yield return new WaitForSeconds(2f);
    }

    IEnumerator switchButtonColors()
    {
        Image buttonImage = GetComponent<Image>();

        if (thisButtonsShape == 0)
        {
            buttonImage.sprite = circleSprite;
        }
        else if (thisButtonsShape == 1)
        {
            buttonImage.sprite = squareSprite;
        }
        else if (thisButtonsShape == 2)
        {
            buttonImage.sprite = triangleSprite;
        }
        else if (thisButtonsShape == 3)
        {
            buttonImage.sprite = heartSprite;
        }

        yield return new WaitForSeconds(2f);

    }
}
