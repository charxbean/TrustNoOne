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

    public static int lives = 3;

    void Start()
    {
        lives = 100;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ThreeLives;
    }

    void FixedUpdate()
    {
        if(LivesScript.lives <= 0)
        {
            StartCoroutine(moveToLosingScene());
        }

        if(LivesScript.lives == 3)
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
    }

    IEnumerator moveToLosingScene()
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(losingScene);
    }

}
