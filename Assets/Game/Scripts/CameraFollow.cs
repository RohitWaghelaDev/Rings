using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
     public Transform player;
    [SerializeField] float lerpSpeed = 100;

    float offsetZ;

    
    private void OnEnable()
    {
        CustomEvents.OnWorldReset += CustomEvents_ResetWorld;
    }

    void CustomEvents_ResetWorld(int worldOffset)
    {
        float zPos = transform.position.z - worldOffset;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
    private void Start()
    {
       // offsetZ = transform.position.z + player.position.z;
    }


    void LateUpdate()
    {
        if (player == null)
        {
            if (GameController.Instance.sceneController.playerTransform)
            {
                player = GameController.Instance.sceneController.playerTransform;
                offsetZ = transform.position.z + player.position.z;
            }
            return;
        }

        FloatLerp();

    }


    float velovityRef = 0;
    public void FloatLerp()
    {

        float targetPositionZ = Mathf.SmoothDamp((player.position.z - offsetZ), transform.position.z, ref velovityRef, lerpSpeed * Time.deltaTime);
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, targetPositionZ);
        transform.position = targetPosition;
    }

}