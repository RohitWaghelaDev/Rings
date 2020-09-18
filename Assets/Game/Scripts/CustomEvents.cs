using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Gamestate : int { LoadOut, Game, GamePause, GameOver };

public class CustomEvents : MonoBehaviour
{
    public delegate void ResetWorld(int worldOffset);
    public static event ResetWorld OnWorldReset;

    public delegate void GameStateChangeEventHandler(Gamestate state);
    public static event GameStateChangeEventHandler OnChangeGameState;

    public delegate void ChangeColorEventHandler(CustomColor color);
    public static event ChangeColorEventHandler OnChangeColorScheme;

    public static void GameStateChanged(Gamestate stateChange)
    {
        OnChangeGameState?.Invoke(stateChange);
    }


    public static void ChageColorScheme(CustomColor color) {

        OnChangeColorScheme?.Invoke(color);
    }
}
