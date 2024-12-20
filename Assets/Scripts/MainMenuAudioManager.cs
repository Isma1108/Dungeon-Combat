using UnityEngine;

public class MainMenuAudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ---------")]
    [SerializeField] AudioSource musicSource;

    [Header("---------- Audio Clip ---------")]
    public AudioClip background;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
