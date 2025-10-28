using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public RewardedAds rewardedAds;
    public static GameManager Instance;

    private void Awake() {
        Instance = this;
    }

    public static GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public Button playButton;
    public Button pauseButton;

    public Button tryAgainButton;

    private bool tryAgainActivated;

    void Start(){
        UpdateGameState(GameState.Pause);
        TryAgainButtonDeactivate();
        tryAgainActivated = false;
    }

    public void UpdateGameState(GameState newState){
        State = newState;
        switch (newState){
            case GameState.GamePlay:
                PlayGame();
                break;
            case GameState.Pause:
                PauseGame();
                break;
            case GameState.GameOver:
                SceneManager.LoadScene(1);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public void PauseGame(){
        Time.timeScale = 0;    
    }

    public void PlayGame(){
        Time.timeScale = 1;
        playButton.gameObject.SetActive(false);
    }

    public void PauseGameButtonActivate(){
        Time.timeScale = 0;
        if(!tryAgainActivated){
            playButton.gameObject.SetActive(true);
        }
    }

    public void PauseButtonDeactivate(){
        pauseButton.gameObject.SetActive(false);
    }

    public void PauseButtonActivate(){
        pauseButton.gameObject.SetActive(true);
    }

    public void TryAgainButtonDeactivate(){
        tryAgainButton.gameObject.SetActive(false);
        tryAgainActivated = false;
    }

    public void TryAgainButtonActivate(){
        tryAgainButton.gameObject.SetActive(true);
        tryAgainActivated = true;
    }

    public void TryAgainActivate(){
        SceneManager.LoadScene(0);
    }
}

public enum GameState {
    GamePlay,
    Pause,
    GameOver
}
