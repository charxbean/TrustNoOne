using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class GameStageManager : MonoBehaviour
{
    private int dialogueIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static int gameStage = 0;
    public static event Action OnStage3Start;
    public static event Action OnStage5Start;
    //dialogue popup texts
    public GameObject[] dialogue;
    public FallingShapeBehavior fallingShapeBehavior;
    public DecoyShapeBehavior decoyShapeBehavior;

    void Start()
    {
        gameStage = 5;
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

            //d1 - welcome
            yield return ShowDialogue(6f);
            HideDialogue();

            //d2 - match to correct shape
            yield return ShowDialogue(5f);
            HideDialogue();

            //start decoy
            //d3 - first up is triangle
            dialogue[dialogueIndex].SetActive(true);
            decoyShapeBehavior.StartDecoy();
            yield return new WaitForSeconds (5f);
            HideDialogue();

            yield return new WaitForSeconds(2f);
            decoyShapeBehavior.StopDecoy();

            //thats not what I asked for
            if(PlayerBehavior.currentShape == 2)
            {
                dialogue[dialogueIndex].SetActive(true);
            }
            else
            {
                dialogueIndex++;
                dialogue[dialogueIndex].SetActive(true);
            }
            yield return new WaitForSeconds(5f);
            dialogue[dialogueIndex].SetActive(false);
            dialogueIndex = 5;

            yield return new WaitForSeconds(3f);

            //alright, lets see how far you make it
            yield return ShowDialogue(4f);
            HideDialogue();

            gameStage = 1;
        }

        if(gameStage == 1)
        {    
            dialogueIndex = 6;
            //MAIN SLOW GAME 
            decoyShapeBehavior.StartDecoy();
            fallingShapeBehavior.moveSpeed = 9f;
            decoyShapeBehavior.showSeconds = 1f;
            decoyShapeBehavior.waitSeconds = 4f;

            yield return new WaitForSeconds(15f);
            
            gameStage = 2;
        }

        if(gameStage == 2)
        {
            //MAIN SPEED UP GAME 
            dialogueIndex = 6;

            //you think your smart huh? Lets see if you can keep up. 
            decoyShapeBehavior.StopDecoy();
            yield return new WaitForSeconds(1f);
            dialogue[dialogueIndex].SetActive(true);
            yield return new WaitForSeconds(4f);

            dialogue[dialogueIndex].SetActive(false);
            dialogueIndex ++;

            fallingShapeBehavior.moveSpeed = 15f;
            decoyShapeBehavior.waitSeconds = 2f;
            decoyShapeBehavior.showSeconds = .5f;
            decoyShapeBehavior.StartDecoy();

            yield return new WaitForSeconds(20f);
            gameStage = 3;
        }

        if(gameStage == 3)
        {   
            //TIME TO SWITCH BUTTONS!!!
            dialogueIndex = 7;
            decoyShapeBehavior.StopDecoy();
            yield return new WaitForSeconds(1f);

            //Okay not bad, lets see if you can handle this...
            yield return ShowDialogue(1f);

            //rearrange UI buttons
            OnStage3Start?.Invoke();
            yield return new WaitForSeconds(2f);
            Debug.Log("Done?");

            HideDialogue();

            //haha!! good luck!
            yield return ShowDialogue(2f);
            HideDialogue();

            fallingShapeBehavior.moveSpeed = 15f;
            decoyShapeBehavior.waitSeconds = 1f;
            decoyShapeBehavior.showSeconds = .5f;
            decoyShapeBehavior.StartDecoy();

            yield return new WaitForSeconds(20f);
            gameStage = 4;
        }
        
        if(gameStage == 4)
        {
            decoyShapeBehavior.StopDecoy();
            dialogueIndex = 9;

            //Faster faster faster!!
            yield return ShowDialogue(2f);
            HideDialogue();

            fallingShapeBehavior.moveSpeed = 17f;
            decoyShapeBehavior.waitSeconds = 1f;
            decoyShapeBehavior.showSeconds = .5f;
            decoyShapeBehavior.StartDecoy();

            yield return new WaitForSeconds(20f);
            gameStage = 5;
            Debug.Log("stage 4");
        }

        if(gameStage == 5)
        {
            decoyShapeBehavior.StopDecoy();
            dialogueIndex = 10;

            //AHH NO more!!
            yield return ShowDialogue(2f);

            OnStage5Start?.Invoke();
            yield return new WaitForSeconds(2f);
            HideDialogue();

            fallingShapeBehavior.moveSpeed = 17.5f;
            decoyShapeBehavior.waitSeconds = 1f;
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
