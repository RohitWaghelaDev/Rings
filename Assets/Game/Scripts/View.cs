using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject loadOutCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject allLevelsClearedCanvas;

    private void OnEnable()
    {
        CustomEvents.OnChangeGameState += CustomEvents_OnChangeGameState;
    }

    private void CustomEvents_OnChangeGameState(Gamestate state)
    {
        switch (state)
        {
            case Gamestate.LoadOut:
                LoadOutView();
                break;
            case Gamestate.Game:
               GameView();
                break;
            case Gamestate.GameOver:
                GameOverView();
                break;


            default:
                break;
        }

        
    }



    private void LoadOutView()
    {
        mainCanvas.SetActive(true);
        loadOutCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        allLevelsClearedCanvas.SetActive(false);
    }

    private void GameOverView()
    {
        mainCanvas.SetActive(true);
        loadOutCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        allLevelsClearedCanvas.SetActive(false);
        Debug.Log("Calling GameOverView");
    }

    private void GameView()
    {
        mainCanvas.SetActive(true);
        loadOutCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        allLevelsClearedCanvas.SetActive(false);
    }


    public void AllLevelsCleared() {
        mainCanvas.SetActive(true);
        loadOutCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        allLevelsClearedCanvas.SetActive(true); ;
    }

    public void OnClickPlayGame() {
        Invoke("StartGame", 0.5f);
    }

    public void OnClickReload()
    {
        SceneManager.LoadScene(0);
    }

    void StartGame() {
        CustomEvents.GameStateChanged(Gamestate.Game);
    }
    private void OnDisable()
    {
        CustomEvents.OnChangeGameState -= CustomEvents_OnChangeGameState;
    }




    
}
