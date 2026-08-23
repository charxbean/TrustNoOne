using Unity.VisualScripting;
using UnityEngine;

public class UIButtonBehavior : MonoBehaviour
{
    public int thisButtonsShape;

    //set global var to a number
    public void SwitchShape()
    {
        Debug.Log("Change shape to" + thisButtonsShape);
        PlayerBehavior.currentShape = thisButtonsShape;
        
    }
}
