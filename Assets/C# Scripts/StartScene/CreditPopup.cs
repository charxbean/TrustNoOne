using UnityEngine;

public class CreditPopup : MonoBehaviour
{
    public GameObject creditCanvas;
    //public AudioManager audioManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCredits()
    {
        creditCanvas.SetActive(true);
    }

    public void CloseCredits()
    {
        creditCanvas.SetActive(false);
    }
}
