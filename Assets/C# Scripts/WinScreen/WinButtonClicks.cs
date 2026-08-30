using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class WinButtonClicks : MonoBehaviour
{
    void Start()
    {
        ButtonClicks.tryAgain = false;
    }
    [SerializeField] private WinAudioManger winningAudioManager;

    public void TryAgain(){
        StartCoroutine(startSceneChange());
        SceneManager.LoadScene("MainGame");
        ButtonClicks.tryAgain = true;
        
    }
    public void backToStartScreen()
    {
        StartCoroutine(startSceneChange());
        SceneManager.LoadScene("StartScreen");
    }

    IEnumerator startSceneChange()
    {
        winningAudioManager.playSFX(winningAudioManager.buttonPress);
        yield return new WaitForSeconds(.5f);
    }
}
