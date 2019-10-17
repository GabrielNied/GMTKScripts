using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public AudioClip menuMusic, mainMusic;
    private AudioSource audioSource;
    private Dictionary<string, AudioClip> audioDict;
    private string musicToPlay, musicPlaying;
    [SerializeField]
    private Slider slide;

    private void Awake()
    {
        
        audioDict = new Dictionary<string, AudioClip>();
        audioDict.Add("Menu", menuMusic);
        audioDict.Add("Principal", mainMusic);
        audioSource = GetComponent<AudioSource>();

        musicToPlay = SceneManager.GetActiveScene().name;

        this.ChangeMusic(musicToPlay);
    }

    private void Update()
    {
        VolumeController();
    }

    public void ChangeMusic (string musicToPlay)
    {
        if (!audioDict.ContainsKey(musicToPlay))
            musicToPlay = "Menu";

        if (musicPlaying == null || 
            !audioDict[musicToPlay].Equals(audioDict[musicPlaying])
            ){
            audioSource.clip = audioDict[musicToPlay];
            audioSource.Play();
            musicPlaying = musicToPlay;
        }
    }

    public void VolumeController()
    {
        audioSource.volume = slide.value;
    }
}
