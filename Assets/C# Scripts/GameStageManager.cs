using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStageManager : MonoBehaviour
{
    private int dialogueIndex = 0;

    private int dialogueStage1n2 = 9;
    private int dialgoueAfterIfStatement = 7;
    private int dialogueStage3 = 10;
    private int dialogueStage4 = 12;
    private int dialogueStage5 = 13;

    public static bool stage4_moveForward = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static int gameStage = 0;
    public static event Action OnStage3Start;
    public static event Action OnStage5Start;

    //dialogue popup texts
    public GameObject[] dialogue;
    public FallingShapeBehavior fallingShapeBehavior;
    public DecoyShapeBehavior decoyShapeBehavior;

    [Header("Animators")]
    public Animator UIButtonAnimator;
    public Animator decoy;

    void Start()
    {
        gameStage = 1;
        StartCoroutine(SetGameStages());
    }

    IEnumerator SetGameStages()
    {   
        for(int i = 0; i < dialogue.Length; i++)
        {
            dialogue[i].SetActive(false);
        }

        yield return new WaitForSeconds(1f);

        if(gameStage == 0)
        {
            //TUTORIAL STAGE

            //d1 - welcome your a circle
            yield return ShowDialogue(0f);
            yield return StartCoroutine(WaitUntilDialogueClicked());
            HideDialogue();

            //click the buttons
            yield return ShowDialogue(0f);
            UIButtonAnimator.SetBool("isTutorial", true);
            yield return new WaitForSeconds(1f);
            UIButtonAnimator.SetBool("isTutorial", false);
            yield return StartCoroutine(WaitForUIButtonClick());
            yield return new WaitForSeconds(1f);
            HideDialogue();

            //d2 - match to correct shape
            yield return ShowDialogue(0f);
            yield return StartCoroutine(WaitUntilDialogueClicked());
            HideDialogue();

            //I'll be giving you hints from right here
            yield return ShowDialogue(0f);
            yield return StartCoroutine(WaitUntilDialogueClicked());
            HideDialogue();

            //start decoy
            //d3 - first up is triangle, ready?
            dialogue[dialogueIndex].SetActive(true);
            decoyShapeBehavior.StartTutorial();
            yield return WaitUntilDialogueClicked();
            HideDialogue();

            //start stage
            decoyShapeBehavior.showSeconds = 1.5f;
            decoyShapeBehavior.StartDecoy();
            yield return new WaitForSeconds(4f);
            decoyShapeBehavior.StopDecoy();
            fallingShapeBehavior.StopBehavior();

            //thats not what I asked for
            decoyShapeBehavior.revealDecoy();
            if(PlayerBehavior.currentShape == 2)
            {
                dialogue[dialogueIndex].SetActive(true);
            }
            else
            {
                dialogueIndex++;
                dialogue[dialogueIndex].SetActive(true);
            }
            yield return new WaitForSeconds(2f);
            dialogue[dialogueIndex].SetActive(false);
            dialogueIndex = dialgoueAfterIfStatement;

            yield return new WaitForSeconds(2f);

            //alright, lets see how far you make it
            yield return ShowDialogue(0f);
            yield return StartCoroutine(WaitUntilDialogueClicked());
            HideDialogue();

            //and make sure to keep listening to what I tell you
            yield return ShowDialogue(0f);
            yield return StartCoroutine(WaitUntilDialogueClicked());
            HideDialogue();

            gameStage = 1;
        }

        if(gameStage == 1)
        {    
            //LivesScript.lives = 3;
            dialogueIndex = dialogueStage1n2;
            //MAIN SLOW GAME 
            decoyShapeBehavior.StartDecoy();
            fallingShapeBehavior.moveSpeed = 9f;
            decoyShapeBehavior.showSeconds = 1f;
            decoyShapeBehavior.waitSeconds = 3f;

            yield return new WaitForSeconds(1f);
            
            gameStage = 2;
        }

        if(gameStage == 2)
        {
            //MAIN SPEED UP GAME 
            dialogueIndex = dialogueStage1n2;

            //you think your smart huh? Lets see if you can keep up. 
            decoyShapeBehavior.StopDecoy();
            decoyShapeBehavior.revealDecoy();
            yield return new WaitForSeconds(1f);
            dialogue[dialogueIndex].SetActive(true);
            yield return StartCoroutine(WaitUntilDialogueClicked());
            //yield return new WaitForSeconds(4f);

            HideDialogue();

            fallingShapeBehavior.moveSpeed = 12f;
            decoyShapeBehavior.waitSeconds = 2f;
            decoyShapeBehavior.showSeconds = .5f;
            decoyShapeBehavior.StartDecoy();

            yield return new WaitForSeconds(1f);
            gameStage = 3;
        }

        if(gameStage == 3)
        {   
            //TIME TO SWITCH BUTTONS!!!
            dialogueIndex = dialogueStage3;
            decoyShapeBehavior.StopDecoy();
            yield return new WaitForSeconds(1f);

            //Okay not bad, lets see if you can handle this...
            decoyShapeBehavior.revealDecoy();
            yield return ShowDialogue(2f);

            //rearrange UI buttons
            OnStage3Start?.Invoke();
            yield return new WaitForSeconds(2f);
            Debug.Log("Done?");

            decoy.SetTrigger("GrowAnim");
            
            yield return StartCoroutine(WaitUntilDialogueClicked());

            HideDialogue();

            //haha!! good luck!
            yield return ShowDialogue(2f);
            HideDialogue();

            fallingShapeBehavior.moveSpeed = 14f;
            decoyShapeBehavior.waitSeconds = 1f;
            decoyShapeBehavior.showSeconds = .5f;
            decoyShapeBehavior.StartDecoy();

            yield return new WaitForSeconds(1f);
            gameStage = 4;
        }
        
        if(gameStage == 4)
        {
            decoyShapeBehavior.StopDecoy();
            decoyShapeBehavior.revealDecoy();
            dialogueIndex = dialogueStage4;

            //Faster faster faster!!
            yield return ShowDialogue(2f);
            HideDialogue();
            stage4_moveForward = true;

            fallingShapeBehavior.moveSpeed = 15f;
            decoyShapeBehavior.waitBeforeSeconds = 0f;
            decoyShapeBehavior.waitSeconds = 2.5f;
            decoyShapeBehavior.showSeconds = 1f;
            decoyShapeBehavior.StartDecoy();

            yield return new WaitForSeconds(1f);
            gameStage = 5;
            Debug.Log("stage 4");
        }

        if(gameStage == 5)
        {
            decoy.SetTrigger("Stage5");
            decoyShapeBehavior.StopDecoy();
            decoyShapeBehavior.revealDecoy();
            dialogueIndex = dialogueStage5;

            //AHH NO more!!
            yield return ShowDialogue(2f);
            OnStage5Start?.Invoke();
            yield return StartCoroutine(WaitUntilDialogueClicked());
            yield return new WaitForSeconds(1f);
            HideDialogue();

            fallingShapeBehavior.moveSpeed = 15.5f;
            decoyShapeBehavior.waitSeconds = .5f;
            decoyShapeBehavior.showSeconds = .5f;
            decoyShapeBehavior.StartDecoy();
            
            yield return new WaitForSeconds(25f);
            gameStage = 6;
        }

        if(gameStage == 6)
        {
            Debug.Log("End of game - Play cutscene");
        }

        else
        {
            Debug.Log("Not a valid game stage number");
        }
        
    }
    IEnumerator WaitUntilDialogueClicked()
    {
        ClickableDialogue.dialogueClicked = false;
        yield return new WaitUntil(() => ClickableDialogue.dialogueClicked);
    }

    IEnumerator WaitForUIButtonClick()
    {
        UIButtonBehavior.buttonClicked = false;
        yield return new WaitUntil(() => UIButtonBehavior.buttonClicked);
    }

    IEnumerator ShowDialogue(float showTime)
    {
        dialogue[dialogueIndex].SetActive(true);
        yield return new WaitForSeconds(showTime);
    }

    private void HideDialogue()
    {
        dialogue[dialogueIndex].SetActive(false);
        dialogueIndex ++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
