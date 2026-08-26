using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class progressbar : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    float currentProgress = 0;
    public float increaseValue = .1f;

    void Start()
    {
        currentProgress = 0;
    }

    public void StartIncreaseProgress()
    {
        StartCoroutine(IncreaseProgressBar());
    } 

    IEnumerator IncreaseProgressBar()
    {
        progressBar.fillAmount += increaseValue;
        currentProgress += 1;
        yield return null;
    }
}

