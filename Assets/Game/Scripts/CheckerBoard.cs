using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerBoard : MonoBehaviour
{
    public int levelId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            LevelCleared();
        }
    }


    void LevelCleared() {
        GameController.Instance.sceneController.clearedLevel = true;
        CustomEvents.GameStateChanged(Gamestate.GameOver);
    }
}
