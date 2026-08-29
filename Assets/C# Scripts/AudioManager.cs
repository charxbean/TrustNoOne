using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Sources")]

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundFX;
    [SerializeField] private AudioSource dialogueSource;


    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip matchGood;
    public AudioClip matchBad;
    public AudioClip dialogue;
    public AudioClip dialogueClick;
    public AudioClip buttonPress;
    public AudioClip addLife;
    public AudioClip decoyStageGrow;
    public AudioClip ButtonsChange;
    public AudioClip fallingShape;
    public AudioClip distractionShapes;
    public AudioClip loseSound;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void playSFX(AudioClip clip)
    {
        float randomPitch = Random.Range(0.93f, 1.10f);
        soundFX.pitch = randomPitch;
        soundFX.PlayOneShot(clip);
    }

    public void playDialogueSFX(AudioClip clip)
    {
        float randomPitch = Random.Range(0.93f, 1.10f);
        dialogueSource.pitch = randomPitch;
        dialogueSource.PlayOneShot(clip);
    }


}
