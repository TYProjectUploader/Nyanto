using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---Audio Source---")]
    [SerializeField] AudioSource bgSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---Bg Audio---")]
    public AudioClip bgMusic;
    [Header("---SFX Audio---")]
    public AudioClip SFXCombine;
    public AudioClip SFXGameOver;
    public AudioClip SFXFishNya;
    public AudioClip SFXMegumin; //EXPLOSION
    public AudioClip SFXButtonClick;

    public static AudioManager instance;
    private void Awake()
    {
        //ensure only one instance exists 
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //makes it so that music keeps playing between scenes
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        bgSource.clip = bgMusic;
        bgSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
