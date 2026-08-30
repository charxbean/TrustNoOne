using System.Collections;
using UnityEngine;

public class DistractionObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] distractionObjects;

    [SerializeField] private AudioManager audioManager;

    void OnEnable()
    {
        //make sure all objects are off to start
        for(int i = 0; i < distractionObjects.Length; i++)
        {
            if(distractionObjects[i] != null)
            {
                distractionObjects[i].SetActive(false);
            }
            else
            {
                Debug.Log("(1) distraction object: " + distractionObjects[i]+ " is null.");
            }
        }

        GameStageManager.OnStage5Distraction += StartShowObjects;
    }

    void StartShowObjects()
    {
        StartCoroutine(ShowObjects());
    }

    public void HideAllDistractions()
    {
        for(int i = 0; i < distractionObjects.Length; i++)
        {
            if(distractionObjects[i] != null)
            {
                distractionObjects[i].SetActive(false);
            }
            else
            {
                Debug.Log("(1) distraction object: " + distractionObjects[i]+ " is null.");
            }
        }
        StopAllCoroutines();
    }

    void OnDestroy()
    {
        GameStageManager.OnStage5Distraction -= StartShowObjects;
        
    }

    IEnumerator ShowObjects()
    {
        for(int i = 0; i < distractionObjects.Length; i++)
        {
            if(distractionObjects[i] == null)
            {
                Debug.Log("(2) distraction object: " + distractionObjects[i]+ " is null.");
            }
            //Debug.Log("enabling distraction object: " + distractionObjects[i]);
            distractionObjects[i].SetActive(true);
            audioManager.playSFX(audioManager.distractionShapes);
            yield return new WaitForSeconds(2f);
        }
    }
}
