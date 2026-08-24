using System.Collections;
using UnityEngine;

public class GameStageManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static int gameStage = 0;

    //dialogue popup texts
    public GameObject[] dialogue;
    public FallingShapeBehavior fallingShapeBehavior;
    public UIButtonBehavior uIButtonBehavior;
    public DecoyShapeBehavior decoyShapeBehavior;
    private int dialogueIndex = 0;
    public static bool buttonPressed = false;


    void Start()
    {
        gameStage = 2;
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
            dialogue[dialogueIndex].SetActive(true);
            yield return new WaitForSeconds(6f);

            dialogue[dialogueIndex].SetActive(false);
            dialogueIndex ++;

            //d2 - match to correct shape
            dialogue[dialogueIndex].SetActive(true);

            yield return new WaitForSeconds(4f);

            dialogue[dialogueIndex].SetActive(false);
            dialogueIndex ++;

            //start decoy
            //d3 - first up is triangle
            dialogue[dialogueIndex].SetActive(true);
            decoyShapeBehavior.StartDecoy();

            yield return new WaitForSeconds (5f);
            dialogue[dialogueIndex].SetActive(false);
            dialogueIndex ++;

            yield return new WaitForSeconds(2f);
            decoyShapeBehavior.StopAllCoroutines();

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

            //alright, lets see how far you make it
            yield return new WaitForSeconds(3f);
            dialogue[dialogueIndex].SetActive(true);

            yield return new WaitForSeconds(4f);
            dialogue[dialogueIndex].SetActive(false);
            dialogueIndex ++;

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
            decoyShapeBehavior.StopAllCoroutines();
            yield return new WaitForSeconds(1f);
            dialogue[dialogueIndex].SetActive(true);
            yield return new WaitForSeconds(4f);

            dialogue[dialogueIndex].SetActive(false);
            dialogueIndex ++;

            fallingShapeBehavior.moveSpeed = 15f;
            decoyShapeBehavior.waitSeconds = 2f;
            decoyShapeBehavior.showSeconds = .5f;
            decoyShapeBehavior.StartDecoy();

            yield return new WaitForSeconds(30f);
            gameStage = 3;
        }

        if(gameStage == 3)
        {   
            //TIME TO SWITCH BUTTONS!!!
            //invoke an action in the uibutton script?
            dialogueIndex = 7;
            decoyShapeBehavior.StopAllCoroutines();
            yield return new WaitForSeconds(1f);

            dialogue[dialogueIndex].SetActive(true);
            yield return new WaitForSeconds(4f);

            
            
        }

        else
        {
            Debug.Log("Not a valid game stage number");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
