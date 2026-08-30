using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClicks : MonoBehaviour
{

    public static bool tryAgain = false;
    void Start()
    {
        tryAgain = false;
    }
    [SerializeField] private LosingAudioManager losingAudioManager;

    public void TryAgain(){
        StartCoroutine(startSceneChange());
        SceneManager.LoadScene("MainGame");
        tryAgain = true;
        
    }
    public void backToStartScreen()
    {
        StartCoroutine(startSceneChange());
        SceneManager.LoadScene("StartScreen");
    }

    IEnumerator startSceneChange()
    {
        losingAudioManager.playSFX(losingAudioManager.buttonPress);
        yield return new WaitForSeconds(.5f);
    }
}
