using UnityEngine;

public class ClickableDialogue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool dialogueClicked = false;
    public void clicked()
    {
        dialogueClicked = true;
    }
}
