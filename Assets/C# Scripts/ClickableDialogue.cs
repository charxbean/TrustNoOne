using UnityEngine;
using UnityEngine.InputSystem;

public class ClickableDialogue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool dialogueClicked = false;
    public Key key;
    public void clicked()
    {
        dialogueClicked = true;
    }

    void Update()
    {
        if (Keyboard.current[key].wasPressedThisFrame)
        {
            dialogueClicked = true;
        }
    }
}
