using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    AudioSource attackSound;
    public AudioSource clockSound;
    public AudioSource enemyLaughSound;
    public AudioSource shopSound;
    public AudioSource correctChoiceSound;
    public AudioSource incorrectChoiceSound;
    public AudioSource gameOverSound;
    public AudioSource enemyDefeatSound;
    public AudioSource letsGoSound;
    public AudioSource fightSound;
    public AudioSource readySound;
    public AudioSource femaleGameOverSound;
    public AudioSource femaleLetsDoSound;
    public AudioSource femaleReadySound;
    public AudioSource femaleEnemyDefeatSound;

    public AudioSource femaleAttackSound;


    private void Awake() 
    {
        Instance = this;
    }

    void Start() 
    {
        attackSound = GetComponent<AudioSource>(); // Ses kaynağı componentinin alınması sound değişkenine atanması
    }

    public void SoundOn()
    {
        attackSound.Play(); // Ses kaynağının oynatılması
    }

    public void FemaleAttackSoundOn()
    {
        femaleAttackSound.Play(); // Ses kaynağının oynatılması
    }

    public void ClockSoundOn(){
        clockSound.Play();
    }


    public void ClockSoundOff(){
        clockSound.Pause();
    }

    public void EnemyLaughSoundOn(){
        enemyLaughSound.Play();
    }

    public void EnemyLaughSoundOff(){
        enemyLaughSound.Pause();
    }

    public void ShopSoundOn(){
        shopSound.Play();
    }

    public void ShopSoundOff(){
        shopSound.Pause();
    }

    public void CorrectChoiceSoundOn(){
        correctChoiceSound.Play();
    }

    public void CorrectChoiceSoundOff(){
        correctChoiceSound.Pause();
    }

    public void IncorrectChoiceSoundOn(){
        incorrectChoiceSound.Play();
    }

    public void IncorrectChoiceSoundOff(){
        incorrectChoiceSound.Pause();
    }

    public void GameOverSoundOn(){
        gameOverSound.Play();
    }

    public void GameOverSoundOff(){
        gameOverSound.Pause();
    }

    public void EnemyDefeatSoundOn(){
        enemyDefeatSound.Play();
    }

    public void EnemyDefeatSoundOff(){
        enemyDefeatSound.Pause();
    }

    public void LetsGoSoundOn(){
        letsGoSound.Play();
    }

    public void LetsGoSoundOff(){
        letsGoSound.Pause();
    }

    public void FightSoundOn(){
        fightSound.Play();
    }

    public void FightSoundOff(){
        fightSound.Pause();
    }

    public void ReadySoundOn(){
        readySound.Play();
    }

    public void ReadySoundOff(){
        readySound.Pause();
    }

    public void FemaleGameOverSoundOn(){
        femaleGameOverSound.Play();
    }

    public void FemaleGameOverSoundOff(){
        femaleGameOverSound.Pause();
    }

    public void FemaleEnemyDefeatSoundOn(){
        femaleEnemyDefeatSound.Play();
    }

    public void FemaleEnemyDefeatSoundOff(){
        femaleEnemyDefeatSound.Pause();
    }

    public void FemaleLetsDoSoundOn(){
        femaleLetsDoSound.Play();
    }

    public void FemaleLetsDoSoundOff(){
        femaleLetsDoSound.Pause();
    }

    public void FemaleReadySoundOn(){
        femaleReadySound.Play();
    }

    public void FemaleReadySoundOff(){
        femaleReadySound.Pause();
    }
}
