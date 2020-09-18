using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject playerGO;
    [SerializeField] LevelData levelData;



    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public bool clearedLevel;
    private void OnEnable()
    {
        CustomEvents.OnChangeGameState += CustomEvents_OnChangeGameState;
    }

    private void CustomEvents_OnChangeGameState(Gamestate state)
    {
        switch (state)
        {
            case Gamestate.LoadOut:
                LoadLevel();
                break;
            case Gamestate.GameOver:
                GameOver();
                break;
            default:
                break;
        }
    }
    private void OnDisable()
    {
        CustomEvents.OnChangeGameState -= CustomEvents_OnChangeGameState;
    }


    

    

private void LoadLevel() {

        if (!PlayerPrefs.HasKey("LevelCleared"))
        {
            PlayerPrefs.SetInt("LevelCleared", 0);
        }

        int levelCleared = PlayerPrefs.GetInt("levelCleared");
        int curentLevel = levelCleared + 1;


        bool levelAvailble = levelData.LevelAvailable(curentLevel);
        if (levelAvailble)
        {
            Level level = levelData.GetLevel(curentLevel);
            SpawnLevel(level);
            SpawnPlayer();
        }
        else
        {
            GameController.Instance.view.AllLevelsCleared();
        }
      

    }

    private void SpawnLevel(Level level) {
        Instantiate(level.levelGO,Vector3.zero,Quaternion.identity);
    }

    public void SpawnPlayer() {
        GameObject temp =  Instantiate(playerGO , new Vector3(0,0,8),Quaternion.identity);
        playerTransform = temp.transform;
    }


    public void GameOver() {
        if (! clearedLevel)
        {
            return;
        }

        int levelCleared = PlayerPrefs.GetInt("levelCleared");
        int curentLevel = levelCleared + 1;

        PlayerPrefs.SetInt("levelCleared",curentLevel);



    }
}
