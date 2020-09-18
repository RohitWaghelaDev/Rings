using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangingWall : MonoBehaviour
{
    public CustomColor wallColor;

    private void Start()
    {
      
       // GetComponent<Renderer>().material.SetColor("_BaseColor", wallColor);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            CustomEvents.ChageColorScheme(wallColor);
            
        }
       
    }
}
