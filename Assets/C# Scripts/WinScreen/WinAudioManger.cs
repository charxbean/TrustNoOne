using UnityEngine;

public class WinAudioManger : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundFX;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip buttonPress;


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
}
