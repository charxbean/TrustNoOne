using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonBehavior : MonoBehaviour
{
    public int thisButtonsShape;


    //set global var to a number
    public void SwitchShape()
    {
        Debug.Log("UI Pressed " + GameStageManager.buttonPressed);
        GameStageManager.buttonPressed = true;
        //Debug.Log("Change shape to" + thisButtonsShape);
        PlayerBehavior.currentShape = thisButtonsShape;
        
    }
}
