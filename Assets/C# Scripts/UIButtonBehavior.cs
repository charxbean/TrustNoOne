using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonBehavior : MonoBehaviour
{
    public int thisButtonsShape;
    public float moveSpeed = 2f;
    
    [Header("Alt UI Sprites")]
    
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite squareSprite;
    [SerializeField] private Sprite triangleSprite;
    [SerializeField] private Sprite heartSprite;
    [SerializeField] private GameStageManager gameStageManager;

    //set global var to a number
    public void SwitchShape()
    {
        //Debug.Log("Change shape to" + thisButtonsShape);
        PlayerBehavior.currentShape = thisButtonsShape;
        
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
        StartCoroutine(switchButtonShapes());
    }
    IEnumerator rearrangeButtons(){
        RectTransform rect = GetComponent<RectTransform>();

        UnityEngine.Vector2 startPosition = rect.anchoredPosition;
        UnityEngine.Vector2 targetPosition = new UnityEngine.Vector2(182, -412);

        if(thisButtonsShape == 0)
        {
            targetPosition = new UnityEngine.Vector2(182, -412);
        }
        else if(thisButtonsShape == 1)
        {
            targetPosition = new UnityEngine.Vector2(-542, -412);
        }
        else if(thisButtonsShape == 2)
        {
            targetPosition = new UnityEngine.Vector2(550, -412);
        }
        else if(thisButtonsShape == 3)
        {
            targetPosition = new UnityEngine.Vector2(-180, -412);
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

    IEnumerator switchButtonShapes()
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
