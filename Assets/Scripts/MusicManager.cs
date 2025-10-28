using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    AudioSource music;
    private bool musicPause;

    private void Awake() 
    {
        Instance = this;
    }

    void Start() 
    {
        music = GetComponent<AudioSource>(); // Müzik componentinin alınması ve music değişkenine atanması
        musicPause = true;
    }

    public void MusicOff()
    {
        music.Pause(); // Müziğin durdurulması
        musicPause = true;
    }

    public void MusicOn()
    {
        music.Play(); // Müziğin oynaması
        musicPause = false;
    }

    public void MusicOnIfPaused(){
        if(musicPause){
            music.Play(); // Müziğin oynaması
        }
    }
}
