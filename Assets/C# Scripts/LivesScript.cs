using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesScript : MonoBehaviour
{
    private string losingScene = "LosingScene";
    private SpriteRenderer spriteRenderer;
    [Header("Lives Sprites")]
    [SerializeField] private Sprite zeroLives;
    [SerializeField] private Sprite oneLife;
    [SerializeField] private Sprite TwoLives;
    [SerializeField] private Sprite ThreeLives;

    public static int lives = 1;

    void Start()
    {
        lives = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(LivesScript.lives <= 0)
        {
            StartCoroutine(moveToLosingScene());
        }
    }

    IEnumerator moveToLosingScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(losingScene);
    }

}
