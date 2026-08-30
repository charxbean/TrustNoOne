using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public static event Action OnStage5Distraction;

    //dialogue popup texts
    public GameObject[] dialogue;
    public FallingShapeBehavior fallingShapeBehavior;
    public winPlayerActivate winActivate;
    public DecoyShapeBehavior decoyShapeBehavior;

    public DistractionObjects distraction;

    public GameObject buttonHighlights;
    public Animator decoy;
    public Animator player;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private SpriteRenderer bgRed;

    [SerializeField] private SpriteRenderer progressMarker1;
    [SerializeField] private SpriteRenderer progressMarker2;
    [SerializeField] private SpriteRenderer progressMarker3;
    [SerializeField] private SpriteRenderer progressMarker4;
    [SerializeField] private SpriteRenderer progressMarker5;
    [SerializeField] private SpriteRenderer progressStar;

    public progressbar progress;

    public void activateProgressMarker(SpriteRenderer pm)
    {
        Color color = pm.color; 
        color.a = 1; 
        pm.color = color;
    }
    void Start()
    {
        if (ButtonClicks.tryAgain)
        {
            gameStage = 1;
            progress.StartIncreaseProgress();
            activateProgressMarker(progressMarker1);
            ButtonClicks.tryAgain = false;

        }
        else
        {
            gameStage = 0;
        }
        StartCoroutine(SetGameStages());
    }

    public void increaseBGRed(SpriteRenderer bgRed, float newAlpha)
    {
        Color color = bgRed.color; 
        color.a = newAlpha; 
        bgRed.color = color;
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
            yield return new WaitForSeconds(1f);
            buttonHighlights.SetActive(true);
            yield return StartCoroutine(WaitForUIButtonClick());
            buttonHighlights.SetActive(false);
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
            audioManager.playDialogueSFX(audioManager.dialogue);
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
                audioManager.playDialogueSFX(audioManager.dialogue);
                dialogue[dialogueIndex].SetActive(true);
            }
            else
            {
                audioManager.playDialogueSFX(audioManager.dialogue);
                dialogueIndex++;
                dialogue[dialogueIndex].SetActive(true);
            }
            yield return new WaitForSeconds(1.5f);
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

            activateProgressMarker(progressMarker1);
            gameStage = 1;
        }

        if(gameStage == 1)
        {    
            LivesScript.lives = 4;
            audioManager.playDialogueSFX(audioManager.addLife);
            dialogueIndex = dialogueStage1n2;
            //MAIN SLOW GAME 
            decoyShapeBehavior.StartDecoy();
            fallingShapeBehavior.moveSpeed = 9f;
            decoyShapeBehavior.showSeconds = 1f;
            decoyShapeBehavior.waitSeconds = 3f;

            yield return new WaitForSeconds(15f);
            
            activateProgressMarker(progressMarker2);
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
            audioManager.playDialogueSFX(audioManager.dialogue);
            yield return StartCoroutine(WaitUntilDialogueClicked());
            //yield return new WaitForSeconds(4f);

            HideDialogue();

            fallingShapeBehavior.moveSpeed = 12f;
            decoyShapeBehavior.waitSeconds = 2f;
            decoyShapeBehavior.showSeconds = .5f;
            decoyShapeBehavior.StartDecoy();

            yield return new WaitForSeconds(20f);
            activateProgressMarker(progressMarker3);
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
            yield return ShowDialogue(1f);

            //rearrange UI buttons
            OnStage3Start?.Invoke();
            audioManager.playSFX(audioManager.ButtonsChange);
            increaseBGRed(bgRed, .05f);
            yield return new WaitForSeconds(2f);
            //Debug.Log("Done?");
            decoy.SetTrigger("GrowAnim");
            audioManager.playSFX(audioManager.decoyStageGrow);
            
            yield return StartCoroutine(WaitUntilDialogueClicked());

            HideDialogue();

            //haha!! good luck!
            yield return ShowDialogue(2f);
            HideDialogue();

            fallingShapeBehavior.moveSpeed = 14f;
            decoyShapeBehavior.waitSeconds = 1f;
            decoyShapeBehavior.showSeconds = .5f;
            decoyShapeBehavior.StartDecoy();

            yield return new WaitForSeconds(20f);
            activateProgressMarker(progressMarker4);
            gameStage = 4;
        }
        
        if(gameStage == 4)
        {
            
            decoyShapeBehavior.StopDecoy();
            decoyShapeBehavior.revealDecoy();
            dialogueIndex = dialogueStage4;

            //Faster faster faster!!
            increaseBGRed(bgRed, .1f);
            yield return ShowDialogue(2f);
            HideDialogue();
            stage4_moveForward = true;

            fallingShapeBehavior.moveSpeed = 15f;
            decoyShapeBehavior.waitBeforeSeconds = 0f;
            decoyShapeBehavior.waitSeconds = 2.5f;
            decoyShapeBehavior.showSeconds = 1f;
            decoyShapeBehavior.StartDecoy();

            yield return new WaitForSeconds(20f);
            activateProgressMarker(progressMarker5);
            gameStage = 5;
            //Debug.Log("stage 4");
        }

        if(gameStage == 5)
        {
            decoy.SetTrigger("Stage5");
            decoyShapeBehavior.StopDecoy();
            decoyShapeBehavior.revealDecoy();
            dialogueIndex = dialogueStage5;

            //AHH NO more!!
            yield return ShowDialogue(1f);
            increaseBGRed(bgRed, .15f);
            buttonHighlights.SetActive(true);
            yield return new WaitForSeconds(1f);
            OnStage5Start?.Invoke();
            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(WaitUntilDialogueClicked());
            buttonHighlights.SetActive(false);
            //yield return new WaitForSeconds(1f);
            HideDialogue();

            fallingShapeBehavior.moveSpeed = 15f;
            decoyShapeBehavior.waitSeconds = .5f;
            decoyShapeBehavior.showSeconds = .5f;
            
            decoyShapeBehavior.StartDecoy();
            yield return new WaitForSeconds(2f);

            OnStage5Distraction?.Invoke();
            

            yield return new WaitForSeconds(25f);
            activateProgressMarker(progressStar);
            audioManager.stopMusic();
            audioManager.playSFX(audioManager.winSound);
            fallingShapeBehavior.EndGame();
            gameStage = 6;
        }

        if(gameStage == 6)
        {
            //Debug.Log("GAME STAGE 6");
            decoyShapeBehavior.StopDecoy();
            decoyShapeBehavior.revealDecoy();
            winActivate.ActivateWinPlayer();
            yield return ShowDialogue(2.5f);
            yield return StartCoroutine(WaitUntilDialogueClicked());
            HideDialogue();
            distraction.HideAllDistractions();
            yield return ShowDialogue(2.5f);
            yield return StartCoroutine(WaitUntilDialogueClicked());
            player.SetTrigger("WIN");
            audioManager.playSFX(audioManager.WinAnimation);
            yield return new WaitForSeconds(3.5f);
            SceneManager.LoadScene("WinScreen");
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
        audioManager.playDialogueSFX(audioManager.dialogue);
        yield return new WaitForSeconds(showTime);
    }

    private void HideDialogue()
    {
        //audioManager.playSFX(audioManager.dialogueClick);
        dialogue[dialogueIndex].SetActive(false);
        dialogueIndex ++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
