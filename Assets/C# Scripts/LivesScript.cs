using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesScript : MonoBehaviour
{
    private string losingScene = "LosingScene";

    private SpriteRenderer spriteRenderer;
    [Header("Lives Sprites")]
    [SerializeField] private Sprite fourLives;
    [SerializeField] private Sprite oneLife;
    [SerializeField] private Sprite TwoLives;
    [SerializeField] private Sprite ThreeLives;
    [SerializeField] private Sprite noLives;

    [SerializeField] private AudioManager audioManager;


    public static int lives = 4;

    void Start()
    {
        lives = 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ThreeLives;
    }

    void FixedUpdate()
    {
        if(LivesScript.lives <= 0)
        {
            StartCoroutine(moveToLosingScene());
        }

        if(LivesScript.lives == 4)
        {
            spriteRenderer.sprite = fourLives;
        }
        else if(LivesScript.lives == 3)
        {
            spriteRenderer.sprite = ThreeLives;
        }
        else if(LivesScript.lives == 2)
        {
            spriteRenderer.sprite = TwoLives;
        }
        else if(LivesScript.lives == 1)
        {
            spriteRenderer.sprite = oneLife;
        }
        else if(LivesScript.lives <= 0)
        {
            spriteRenderer.sprite = noLives;
        }
    }

    IEnumerator moveToLosingScene()
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(losingScene);
    }

}
