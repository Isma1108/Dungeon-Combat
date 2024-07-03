
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ---------")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("---------- Audio Clip ---------")]
    public AudioClip background;
    public AudioClip swordAttack;
    public AudioClip bowAttack;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.pitch = Random.Range(0.8f, 1.2f);
        SFXSource.PlayOneShot(clip);
    }
}
