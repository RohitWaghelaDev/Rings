using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CustomColor : int { Red,Green,Yellow}

public class Ring : MonoBehaviour
{

    int maxDistanceFromPlayer = 3 ;
    int playerDeadDistance = 2;

    bool interactedWithPlayer;
    public CustomColor ringColor;

   [SerializeField] PlayerController PlayerController;
    Gamestate currentGameState;
    private void OnEnable()
    {
        CustomEvents.OnChangeGameState += CustomEvents_OnChangeGameState;
        

    }
    private void CustomEvents_OnChangeGameState(Gamestate state)
    {
        currentGameState = state;
    }

    private void OnDisable()
    {
        CustomEvents.OnChangeGameState -= CustomEvents_OnChangeGameState;
        
    }


    // Update is called once per frame
    void Update()
    {
        if (currentGameState != Gamestate.Game)
        {
            return;
        }
        if (PlayerController == null)
        {
            if (GameController.Instance.sceneController.playerTransform != null)
            {
                PlayerController = GameController.Instance.sceneController.playerTransform.gameObject.GetComponent<PlayerController>();
            }
            return;
        }

        float distanceFromPlayer = GameController.Instance.sceneController.playerTransform.position.z- transform.parent.position.z;
        if (PlayerController.GetColor() == ringColor
            && GameController.Instance.sceneController.playerTransform.position.z > transform.position.z
            && distanceFromPlayer < maxDistanceFromPlayer)
        {
            
            if (distanceFromPlayer >= playerDeadDistance && !interactedWithPlayer)
            {
                PlayerController.Playerdead();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameController.Instance.view.OnClickReload();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger " + other.gameObject.name);
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            if (playerController.GetColor() == ringColor)
            {
                interactedWithPlayer = true;
            }
            else
            {
               playerController.Playerdead();
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("collision " + collision.gameObject.name);
    //    PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
    //    if (playerController)
    //    {
    //        playerController.Playerdead();
    //    }
    //}
}
