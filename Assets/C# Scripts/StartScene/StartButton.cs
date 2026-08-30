using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private StartAudioManager audioManager;
    public GameObject creditCanvas;
    public GameObject StartButtonCanvas;
    public void StartGame()
    {
        StartCoroutine(pressStartButton());
    }

    IEnumerator pressStartButton()
    {
        audioManager.playSFX(audioManager.startButton);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("MainGame");
    }

    public void OpenCredits()
    {
        creditCanvas.SetActive(true);
        StartButtonCanvas.SetActive(false);
    }

    public void CloseCredits()
    {
        creditCanvas.SetActive(false);
        StartButtonCanvas.SetActive(true);
    }
}
