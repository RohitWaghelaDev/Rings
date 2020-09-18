using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [HideInInspector] public SceneController sceneController;
    [HideInInspector] public View view;

    private void Awake()
    {
        if (Instance == null )
        {
            Instance = GetComponent<GameController>();
        }
        sceneController = GetComponent<SceneController>();
        view = GetComponent<View>();
    }

    private void Start()
    {
        CustomEvents.GameStateChanged(Gamestate.LoadOut);
        
    }
}
